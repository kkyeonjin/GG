using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System;

public class CompetitorAgent : Agent
{
    /// <summary>
    /// Variable : Status / Move / Avoid / Hide / Item / Environment 총 6부문별로 구분
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
    public const float runStamina = 25f;
    /// 스태미나 회복속도 <see cref="CharacterStatus.m_fSPRecover"/>
    public const float staminaRecover = 10f;


    //2. Move : Agent의 action에 관한 변수
    // Rigidbody & Animator
    public Rigidbody agentRb;
    private Animator agentAnimator;

    /// 이동속도 <see cref="Player.m_fSpeed"/>
    public const float moveForce = 50f; //Player.m_fspeed

    /// 회전속도 <see cref="Player.m_fRotateSpeed"/>
    public const float rotationSpeed = 250f;
    private float smoothRotationChange = 0f; // 자연스러운 회전을 위한 계수

    /// 달리기 중 여부 <see cref="Player.m_bIsRun"/>
    private bool isRun;

    public CheckpointManager checkpointManager;


    //3. Avoid : 주변 사람들을 피하면서 
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


    //4. Hide
    // 지진 이벤트 발생 여부
    public bool eventOccured;

    // 가장 가까이 있는 bunker
    private Bunker nearestBunker;

    // bunker에 숨는 중인지 여부
    private bool isHide;


    //5. Environment
    //Training Mode 여부
    public bool trainingMode = true;

    //Agent가 의도적으로 움직임을 멈춘 상태인지 여부
    private bool frozen = false;

    //Agent가 속해 있는 맵 영역
    public SubwayArea subwayArea;

    //6. Item



    ///<summary>
    ///<see cref="CharacterStatus"/>
    ///     Status private 변수 접근자 & HP 및 스태미나 관련 함수
    ///</summary>

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
    public void Use_Stamina(float runStamina)
    {
        currentStamina -= runStamina * Time.deltaTime;
        if (0f > currentStamina)
        {
            currentStamina = 0;
            staminaIsUsable = false;
        }
    }

    private void Recover_Stamina()
    {
        currentStamina += staminaRecover * Time.deltaTime;
        if (currentStamina > 10f)
            staminaIsUsable = true;

        if (currentStamina > maxStamina)
            currentStamina = maxStamina;
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

    private void Start()
    {

    }

    private void Update()
    {
        Recover_Stamina();
    }



    ///<summary>
    /// ML agent 상속 함수
    ///</summary>

    /// <summary> Initialize the agent </summary>
    public override void Initialize()
    {
        agentRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        agentCamera = GetComponentInChildren<Camera>();
        subwayArea = GetComponentInParent<SubwayArea>();
        checkpointManager = GetComponentInChildren<CheckpointManager>();

        //If not training mode, no max step and keep playing
        if (!trainingMode) MaxStep = 0;
    }

    /// <summary> Reset the agent when an episode begins </summary>
    public override void OnEpisodeBegin()
    {
        if (trainingMode)
        {
            //agent 스폰 위치 설정
            MoveToSafeRandomPosition();

            //낙하물, 아이템 위치 등 리셋 (Hide, Item 개발시)
            //subwayArea.ResetMap();
        }

        //Status 초기화
        currentHP = maxHP;
        currentStamina = maxStamina;
        this.gameObject.SetActive(true);

        //Move 초기화
        agentRb.velocity = Vector3.zero;
        agentRb.angularVelocity = Vector3.zero;

        //Checkpoint 초기화
        checkpointManager.ResetCheckpoints();

    }

    /// <summary> Move agent to a position
    /// where agent does not collide with anything
    /// </summary>
    private void MoveToSafeRandomPosition()
    {
        bool safePositionFound = false;
        int attemptRemaining = 100;
        Vector3 potentialPosition = Vector3.zero;
        //Quaternion potentialRotation = new Quaternion();

        potentialPosition = subwayArea.returnRandomStartingPosition();
        transform.position = potentialPosition;
        Debug.Log(this.gameObject.name + " respawn");

        /*
        // 시도 횟수 남아있을 동안만 safe position 탐색
        while (!safePositionFound && attemptRemaining > 0)
        {
            //Debug.Log(attemptRemaining);
            attemptRemaining--;

            // subwayArea로부터 스타팅 구역 내 랜덤 위치 받아오기
            potentialPosition = subwayArea.returnRandomStartingPosition();
            //Debug.Log("potential position: " + potentialPosition);

            // 주변 오브젝트와 collider 겹치는지 (즉 충돌하는지) 확인
            Collider[] colliders = Physics.OverlapSphere(potentialPosition, 0.5f);
            //foreach (Collider collider in colliders)
            //{
            //    Debug.Log("Collider found: " + collider.name);
            //}

            // startingArea외 겹치는 collider 없으면 스폰위치 설정 성공
            // safePositionFound = colliders.Length == 0;
            if (colliders.Length == 1 && colliders[0].name == "StartingPoint")
            {
                safePositionFound = true;
                break;
            }
        }

        if (safePositionFound)
        {
            transform.position = potentialPosition;
        }
        else
        {
            Debug.Assert(safePositionFound, "Could not find safe respawn position");
        }
        */
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (trainingMode)
        {
            /*
            //벽과 충돌시 감점 
            if (collision.collider.CompareTag("Wall")){
                AddReward(-0.5f);
            }

            //AI와 충돌시 감점
            if (collision.collider.CompareTag("AI"))
            {
                AddReward(-0.2f);
            }
            */
            if (!collision.collider.CompareTag("Checkpoint"))
            {
                if (collision.collider.CompareTag("Wall"))
                {
                    AddReward(-0.5f);
                }
                else if (collision.collider.CompareTag("AI"))
                {
                    AddReward(-0.2f);
                }
                else
                {
                    AddReward(-0.3f);
                }
            }
        }
    }

    //RaycastSensors로부터 수집되지 않는 정보를 수집함
    public override void CollectObservations(VectorSensor sensor)
    {
        // agent의 현재 회전 방향 (SubwayArea 상에서의 상대적 방향) -> 4 observations
        sensor.AddObservation(transform.localRotation.normalized);

        // agent와 다음 checkpoint 사이의 벡터 -> 3 observations
        Vector3 diff = checkpointManager.nextCheckPointToReach.transform.position - transform.position;
        sensor.AddObservation(diff.normalized);
    }

    /// <summary>
    /// action 명령이 player input이나 NN으로부터 들어오면 호출됨
    /// * 일단 y 벡터 (점프) 제외
    ///
    /// 1. ContinuousActions[i]
    /// [0] : move vector x (+1 = right, -1 = left)
    /// [1] : move vector z (+1 = forward, -1 = backward)
    /// [2] : yaw angle (+1 = turn right, -1 = turn left)
    ///
    /// 2. DiscreteActions[i]
    /// [0] : isRunning (1: 달리는 중, 0: 
    /// </summary>
    /// <param name="actions"></param>
    public override void OnActionReceived(ActionBuffers actions)
    {

        //action 호출할 때마다 패널티를 부여하여
        //action을 줄이도록 (즉 에피소드를 빠르게 클리어하도록) 유도
        AddReward(-0.01f);

        //Don't take actions if frozen
        if (frozen) return;

        /*
        //Calculate movement vector
        Vector3 move = new Vector3(actions.ContinuousActions[0], 0, actions.ContinuousActions[1]);
        //bool isRunning = actions.DiscreteActions[0] == 0;

        //Add force in the direction of the move vector
        agentRb.AddForce(move * moveForce);

        //Get the current rotation
        Vector3 rotationVector = transform.rotation.eulerAngles;

        //Caculate yaw rotation
        float rotationChange = actions.ContinuousActions[2];

        //Calculate smooth rotation changes
        smoothRotationChange = Mathf.MoveTowards(smoothRotationChange, rotationChange, 2f * Time.fixedDeltaTime);

        //Calculate new yaw based on smoothed values
        float rotation = rotationVector.y + smoothRotationChange * Time.fixedDeltaTime * rotationSpeed;

        //Apply the new rotation
        transform.rotation = Quaternion.Euler(0, rotation, 0f);   
        */

        var dir = Vector3.zero;
        var rot = Vector3.zero;
        var action = actions.DiscreteActions[0];
        switch (action)
        {
            case 1:
                dir = transform.forward * 1f;
                break;
            case 2:
                dir = transform.forward * -1f;
                break;
            case 3:
                rot = transform.up * 1f;
                break;
            case 4:
                rot = transform.up * -1f;
                break;
        }
        transform.Rotate(rot, Time.deltaTime * 100f);

        agentRb.AddForce(dir * 0.4f, ForceMode.VelocityChange);

        //this.transform.position += transform.forward * Time.deltaTime * 5f;
        animator.SetBool("IsRun", agentRb.velocity != Vector3.zero);

    }

    /// <summary>
    /// <see cref="OnActionReceived(ActionBuffers)"에 NN model 대신 키보드 인풋 값을 넘겨줌/>
    /// </summary>
    /// <param name="actionsOut">Output action array</param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        /*
        var action = actionsOut.ContinuousActions;

        Vector3 fb = Vector3.zero;
        Vector3 lr = Vector3.zero;
        float yaw = 0f;

        // 전후 이동
        if (Input.GetKey(KeyCode.W)) fb = transform.forward;
        else if (Input.GetKey(KeyCode.S)) fb = -transform.forward;

        // 좌우 이동
        if (Input.GetKey(KeyCode.A)) lr = -transform.right;
        else if (Input.GetKey(KeyCode.D)) lr = transform.right;

        // 좌우 방향 전환
        if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

        // 벡터 합친 후 정규화
        Vector3 combined = (fb + lr).normalized;

        //action에 전달
        action[0] = combined.x;
        action[1] = combined.z;
        action[2] = yaw;
    }
    */
        var discreteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
    }
}
