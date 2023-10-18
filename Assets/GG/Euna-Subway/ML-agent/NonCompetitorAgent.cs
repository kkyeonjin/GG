using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class NonCompetitorAgent : Agent
{
    /// <summary>
    /// Variable : Status / Move / Environment / Avoid / Hide / Item 총 6부문별로 구분
    /// </summary>

   //1. Status : Agent 상태값
    public CompetitorAgent agent;
    private Animator animator;

    // agent 시점 카메라
    public Camera agentCamera;

    /// 최대 HP량 <see cref="CharacterStatus.m_fMaxHP"/>
    public const float maxHP = 100f;
    /// 현재 HP량 <see cref="CharacterStatus.m_fHP"/>
    private float currentHP;


   //2. Move : Agent의 action에 관한 변수
    // Rigidbody & Animator
    public Rigidbody agentRb;
    private Animator agentAnimator;

    /// 이동속도 <see cref="Player.m_fSpeed"/>
    public float currentSpeed = 3f; //Player.m_fspeed
    public const float walkSpeed = 3f;
    public const float runSpeed = walkSpeed * 1.5f;

    // 점프 스케일 & 중력
    public float jumpForce = 3f;
    public Vector3 customGravity = new Vector3(0, -19.62f, 0);

    //Agent가 의도적으로 움직임을 멈춘 상태인지 여부
    private bool frozen = false;
    /// 지면 상에 있는지 여부 <see cref="Player.m_bIsGround"/>
    private bool isGround;

    /// 달리기 중 여부 <see cref="Player.m_bIsRun"/>
    private bool isRun = true;
    /// 점프 중인지 여부 <see cref="Player.m_bIsJump"/>
    private bool isJump;

    public float _rotate = 80f;


   //3. Environment
    //Training Mode 여부
    public bool trainingMode = true;

    //Agent의 훈련 시작 위치
    private Vector3 startingPoint;

    //Delta_reward
    private Vector3 _destination = new Vector3(70.6200027f, 5.32999992f, 160.669998f);
    float _prevDistance;
    float _nextDistance;

    //Reward Shaping
    float _totalDistance;

    //Obs
    public List<GameObject> _agents;
    public List<float> cal_agents;


    public void Set_Dead()
    {
        agentAnimator.SetTrigger("Death");
    }

    private void Update()
    {
               
    }

    public override void Initialize()
    {
        agentRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        agentCamera = GetComponentInChildren<Camera>();
        startingPoint = this.transform.position;
        Physics.gravity = customGravity;

        //If not training mode, no max step and keep playing
        if (!trainingMode) MaxStep = 0; 
    }

    public override void OnEpisodeBegin()
    {
        if (trainingMode)
        {
            //agent 스폰
            this.transform.position = startingPoint;

            transform.rotation = Quaternion.Euler(0, 90, 0);
            _prevDistance = Vector3.Distance(_destination, startingPoint);
            _totalDistance = Vector3.Distance(_destination, startingPoint);
            //낙하물, 아이템 위치 등 리셋 (Hide, Item 개발시)
            //subwayArea.ResetMap();
        }

        //Move 초기화
        agentRb.velocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("goal"))
        {
            AddReward(100f);
            Debug.Log("Goal");
            EndEpisode();
        }

        if (col.CompareTag("Checkpoint"))
        {
            AddReward(10f);
            Debug.Log("Get the reward");
        }

        if (col.CompareTag("Block"))
        {
            SetReward(-30f);
            Debug.Log("Hit Block Trigger");
            EndEpisode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //actionMove 및 animation 관련

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("stair"))
        {
            if (collision.gameObject.CompareTag("stair"))
            {
                agentRb.AddForce(transform.forward * currentSpeed * 0.4f, ForceMode.VelocityChange);
                AddReward(10f);
            }
            isJump = false;
            animator.SetBool("IsJump", isJump);
            isGround = true;
            animator.SetBool("IsGround", isGround);
        }
        if (collision.gameObject.CompareTag("Falling"))
        {
            SetReward(-30f);
            Set_Dead();
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AddReward(-5f);
        }

        //훈련 관련
        if (trainingMode)
        {
            Collider col = collision.collider;

            if (col.CompareTag("Wall"))
            {
                SetReward(-30f);
                Debug.Log("Hit Wall");
                EndEpisode();
            }

            if (col.CompareTag("Block"))
            {
                SetReward(-30f);
                Debug.Log("Hit Block Collider");
                EndEpisode();
            }

            if (col.CompareTag("AI"))
            {
                AddReward(-1f);
            }
            /*
            이후 Obstacle, Falling, Item 추가
            */
        }
    }

    //RaycastSensors로부터 수집되지 않는 정보를 수집함
    public override void CollectObservations(VectorSensor sensor)
    {
        //Goal 지점까지의 거리 (DeltaReward)
        sensor.AddObservation(_nextDistance); //도착지와의 거리 (변수명 재활용)
        for (int i = 0; i < _agents.Count; i++)
        {
            sensor.AddObservation(cal_agents[i]);
        }
        //
    }

    //Revised (9.24.)
    public override void OnActionReceived(ActionBuffers actions)
    {
        
        //Decision Requester 없애고 해야 하실 거에요
        //agent.RequestDecision();
        //Goal 지점까지의 거리 (DeltaReward)
        _nextDistance = Vector3.Distance(transform.position, _destination);
        //Debug.Log("staminaIsUsable : " + staminaIsUsable);
        //Debug.Log("이전거리 : " + _prevDistance + "현재 거리 : " + _nextDistance);

        //DeltaReward
        /*if (_nextDistance < _prevDistance) { AddReward(0.01f); }
        else { AddReward(-0.002f); }
        _prevDistance = _nextDistance;*/

        //GradualReward
        AddReward(((_totalDistance -_nextDistance)/_totalDistance));
        

        //관측
        _calDistance();

        //actionMove 호출할 때마다 패널티를 부여하여
        //action을 줄이도록 (즉 에피소드를 빠르게 클리어하도록) 유도
        AddReward(-0.05f);

        var dir = Vector3.zero;
        dir += transform.forward;

        //DiscreteAction[0] 앞으로 이동 여부 2

        /*switch (actionForward)
        {
            //Heuristic 테스트 외에는 case 0 삭제 
            case 0:
                frozen = true;
                //Debug.Log("Frozen");
                break;
            case 1:
                frozen = false;
                dir += transform.forward;
                break;
        }*/

        //DiscreteAction[0] 전진 여부 2
        var actionRun = actions.DiscreteActions[1];
        switch (actionRun)
        {
            case 0:
                break;
            case 1:
                animator.SetBool("IsRun", isRun);
                animator.SetBool("IsSprint", isRun);
                break;
        }


        //DiscreteAction[1] 이동 방향 (좌:0 우:1)
        var actionDir = actions.DiscreteActions[0];

        switch (actionDir)
        {
            case 0:
                //dir -= transform.right;
                transform.Rotate(Vector3.up, Time.deltaTime * -_rotate);
                break;
            case 1:
                //dir += transform.right;
                transform.Rotate(Vector3.up, Time.deltaTime * _rotate);
                break;
        }


        //Debug.Log("isRun : " + isRun);
        //이동 (MovePosition 적용)
        agentRb.MovePosition(transform.position + dir * currentSpeed * Time.deltaTime);

        //DiscreteAction[2] 점프 여부 2
        var actionJump = actions.DiscreteActions[2];
        if (actionJump == 1 && !isJump)
        {
            agentRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJump = true;
            isGround = false;
            isRun = false;
            animator.SetBool("isJump", isJump);
            animator.SetTrigger("Jump");
            animator.SetBool("isGround", isGround);
        }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.DiscreteActions;

        //0. 앞으로 이동 여부
        if (Input.GetKey(KeyCode.W))
        {
            action[0] = 1;
        }

        //1. 좌우 방향 (좌:0 우:1)
        if (Input.GetKey(KeyCode.A))
        {
            action[1] = 0;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            action[1] = 1;
        }

        //2. 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            action[2] = 1;
        }
    }

    void _calDistance()
    {
        cal_agents = new List<float>();
        for (int i = 0; i < _agents.Count; i++)
        {
            float d;
            d = Vector3.Distance(_agents[i].transform.position, transform.position);
            cal_agents.Add(d);
        }
    }


    /*
    //4. Avoid : 주변 사람들을 피하면서 
    // 근접하다고 인식할 반경
    public const float closeRadius = 1f;

    // 충돌했다고 인식할 반경
    public const float collideRadius = 0.5f; //AddReward(-1)

    // 반경 내 Human (AI & Players)
    public CompetitorAgent[] closeAIs;
    public Player[] closePlayers;

    // 반경 내 장애물 (Fall & Obstacle)
    //public GameObject closeFall;
    //public GameObject closeObstacle;


    //5. Hide
    // 지진 이벤트 발생 여부
    public bool eventOccured;

    // 가장 가까이 있는 bunker
    private Bunker nearestBunker;

    // bunker에 숨는 중인지 여부
    private bool isHide;

    //6. Item
    */
}


