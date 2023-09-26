using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class CompetitorAgent : Agent
{
    /// euna
    /// <summary>
    /// Variable : Status / Move / Environment / Avoid / Hide / Item 총 6부문별로 구분
    /// </summary>

   //1. Status : Agent 상태값
    public CompetitorAgent agent;
    public Animator animator;

    // agent 시점 카메라
    public Camera agentCamera;

    /// 최대 HP량 <see cref="CharacterStatus.m_fMaxHP"/>
    public const float maxHP = 100f;
    /// 현재 HP량 <see cref="CharacterStatus.m_fHP"/>
    private float currentHP;

    /// 최대 스태미나량 <see cref="CharacterStatus.m_fMaxStamina"/>
    public const float maxStamina = 100f;
    /// 현재 스태미나량 <see cref="CharacterStatus.m_fStamina"/>
    private float currentStamina;
    /// 스태미나 사용 가능 여부 <see cref="CharacterStatus.m_bIsUsable"/>
    private bool staminaIsUsable = true;
    /// 스태미나 소모 속도 (달리기) <see cref="Player.Run"/>
    public const float staminaRate = 25f;
    /// 스태미나 회복속도 <see cref="CharacterStatus.m_fSPRecover"/>
    public const float staminaRecover = 20f;


   //2. Move : Agent의 action에 관한 변수
    // Rigidbody & Animator
    public Rigidbody agentRb;
    private Animator agentAnimator;

    /// 이동속도 <see cref="Player.m_fSpeed"/>
    public float currentSpeed = 5f; //Player.m_fspeed
    public const float walkSpeed = 5f;
    public const float runSpeed = walkSpeed * 1.5f;

    // 점프 스케일 & 중력
    public float jumpForce = 250f;
    public Vector3 customGravity = new Vector3(0, -19.62f, 0);

    //Agent가 의도적으로 움직임을 멈춘 상태인지 여부
    private bool frozen = false;
    /// 지면 상에 있는지 여부 <see cref="Player.m_bIsGround"/>
    private bool isGround;
    /// 달리기 중 여부 <see cref="Player.m_bIsRun"/>
    private bool isRun;
    /// 점프 중인지 여부 <see cref="Player.m_bIsJump"/>
    private bool isJump;


   //3. Environment
    //Training Mode 여부
    public bool trainingMode = true;

    //Agent의 훈련 시작 위치
    private Vector3 startingPoint;



    public float Get_HP()
    {
        return currentHP;
    }
    public float Get_Stamina()
    {
        return currentStamina;
    }

    public void Set_Damage(float fDamage)
    {
        //m_PV.RPC("Damaging", RpcTarget.All, fDamage);
    }
    public bool Is_Usable()
    {
        return staminaIsUsable;
    }

    public void Use_Stamina()
    {
        if (currentStamina <= 0f)
        {
            currentStamina = 0f;
            staminaIsUsable = false;
            isRun = false;
            return;
        }
        Debug.Log("use stamina");
        currentStamina -= staminaRate * Time.deltaTime;
    }

    private void Recover_Stamina()
    {
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
            return;
        }

        currentStamina += staminaRecover * Time.deltaTime;
        Debug.Log("recover stamina");

        if (currentStamina > 10f)
        {
            staminaIsUsable = true;
        }
    }

    private void Damaging(float fDamage)
    {
        currentHP -= fDamage;
        if (0f >= currentHP)
        {
            currentHP = 0;
            this.Set_Dead();
        }
    }

    public void Set_Dead()
    {
        agentAnimator.SetTrigger("Death");
        this.gameObject.SetActive(true);
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
            Debug.Log("New Episode");
            //agent 스폰
            this.transform.position = startingPoint;

            //낙하물, 아이템 위치 등 리셋 (Hide, Item 개발시)
            //subwayArea.ResetMap();
        }

        //Status 초기화
        currentHP = maxHP;
        currentStamina = maxStamina;

        //Move 초기화
        agentRb.velocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;
    }


    public void OnCollisionEnter(Collision collision)
    {
        //actionMove 및 animation 관련
        if (collision.gameObject.CompareTag("stair"))
        {
            agentRb.AddForce(transform.forward * currentSpeed * 0.4f, ForceMode.VelocityChange);
        }
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("stair"))
        {
            isJump = false;
            animator.SetBool("IsJump", isJump);
            isGround = true;
            animator.SetBool("IsGround", isGround);
        }

        //훈련 관련
        if (trainingMode)
        {
            Collider col = collision.collider;

            if (col.CompareTag("Wall"))
            {
                SetReward(-1f);
                EndEpisode();
            }

            if (col.CompareTag("AI"))
            {
                AddReward(-0.5f);
            }

            if (collision.collider.CompareTag("Goal"))
            {
                AddReward(5f);
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

        //
    }

    //Revised (9.24.)
    public override void OnActionReceived(ActionBuffers actions)
    {
        //actionMove 호출할 때마다 패널티를 부여하여
        //action을 줄이도록 (즉 에피소드를 빠르게 클리어하도록) 유도
        AddReward(-0.01f);

        var dir = Vector3.zero;

        //DiscreteAction[0] 앞으로 이동 여부 2
        var actionForward = actions.DiscreteActions[0];
        switch (actionForward)
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
        }

        //DiscreteAction[1] 이동 방향 (좌:0 우:1)
        var actionDir = actions.DiscreteActions[1];
        switch (actionDir)
        {
            case 0:
                break;
            case 1:
                dir -= transform.right;
                break;
            case 2:
                dir += transform.right;
                break;
        }

        //DiscreteAction[2] 달리기 여부 2
        var actionRun = actions.DiscreteActions[2];

        
        switch (actionRun)
        {
            case 0:
                isRun = false;
                currentSpeed = walkSpeed;
                Debug.Log("walk");
                break;
            case 1:
                if (!isRun && staminaIsUsable)
                {
                    currentSpeed = runSpeed;
                    isRun = true;
                    Debug.Log("Run");
                }
                break;
        }

        //이동 (MovePosition 적용)
        agentRb.MovePosition(transform.position + dir * currentSpeed * Time.deltaTime);

        //스태미나
        if (isRun) Use_Stamina();
        else Recover_Stamina();
        Debug.Log("stamina : " + currentStamina);

        /*
        if (actionRun == 1 && !isRun && staminaIsUsable)
        {
            currentSpeed *= 1.5f;
            isRun = true;
            //Use_Stamina();
        }
        */


        //DiscreteAction[3] 점프 여부 2
        var actionJump = actions.DiscreteActions[3];
        if (actionJump == 1 && !isJump)
        {
            agentRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJump = true;
        }

        //애니메이터?? 다시 확인
        //animator.SetBool("IsRun", agentRb.velocity != Vector3.zero);

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
            action[1] = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            action[1] = 2;
        }

        //2. 달리기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            action[2] = 1;
        }

        //3. 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            action[3] = 1;
        }
    }



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
}


