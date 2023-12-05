using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.Diagnostics.Eventing.Reader;
using Unity.MLAgents;

public class Player : MonoBehaviour
{

    public enum CHARACTER { BOY, GLASSES, REDHAIR, OLDWOMAN, WIZARD, ALIEN, ZOMBIE, ASTRONAUT, VAMPIRE, END };

    public float m_fSpeed;
    public float m_fJumpScale = 100;
    public EventUI m_ClearUI;
    public float m_fRotateSpeed = 250f;
    public PhotonView m_PV = null;

    private float m_fXRotate, m_fYRotate;
    private float m_XTotalRot, m_YTotalrot;

    private Rigidbody m_Rigidbody;
    private Vector3 m_vMoveVec;
    private Animator m_Animator;

    private bool m_bIsRun = false;
    private bool m_bIsSprint = false;
    private bool m_bIsJump = false;
    private bool m_bIsGround = true;
    private bool m_bIsCrouch = false;
    public bool m_bIsThrow = false;

    private bool m_bJumpAvailable = true;

    private float m_fTotalSpeed;
    private float m_fJumpForce;

    private CharacterStatus m_Status;
    public Transform m_CameraTransform;

    private delegate void MoveFunc();

    private MoveFunc m_Moving;

    private bool m_bInteract_Column = false;
    private bool m_bInteract_Lever = false;
    private bool m_bInteract_EnterCode = false;

    private Interactive Curr_InteractiveObj;

    private bool m_bHolding = false;

    private bool m_bAdrenaline = false;
    private bool m_bInvincible = false;

    public GameObject OnHand;

    private PlayerEffect m_Effect;

    //싱글모드 경사로
    private RaycastHit slopeHit;
    private int groundlayer;
    private float maxSlopeAngle = 45f;
    void Start()
    {
        m_Status = GetComponentInChildren<CharacterStatus>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();

        if (m_PV != null)
        {
            if (m_PV.IsMine)
            {
                GameMgr.Instance.Set_LocalPlayer(this);
                GameMgr.Instance.Set_Camera();
                m_Effect = GetComponentInChildren<PlayerEffect>();

                m_bJumpAvailable = GameMgr.Instance.m_bInGame;
                this.gameObject.layer = LayerMask.NameToLayer("Player");
                this.gameObject.tag = "Player";
            }
            else
            {
                this.gameObject.layer = LayerMask.NameToLayer("OtherPlayer");
                this.gameObject.tag = "OtherPlayer";
            }


            m_Moving = new MoveFunc(Move_MultiMode);

        }
        else
        {
            
            m_Moving = new MoveFunc(Move_SingleMode);
            groundlayer = 1 << LayerMask.NameToLayer("Ground");
        }

    }
    
    public void Off_Jump()
    {
        m_bJumpAvailable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            m_Status.Set_Damage(1f);
            Debug.Log("Player HP :" + m_Status.Get_HP());
        }
        m_Moving();
    }

    public void Change_Animator(Animator In_Animator)
    {
        m_Animator = In_Animator;
    }

    public void Change_Status(AvatarStatus Input)
    {
        m_fSpeed = Input.Get_Speed();
        m_Status.Change_Status(Input.Get_HP(), Input.Get_Stamina());
        OnHand = Input.Get_Hand();
    }


    public void Set_Camera(CinemachineVirtualCamera In_Camera)
    {
        if (m_PV.IsMine)
        {
            In_Camera.Follow = this.transform;
            In_Camera.LookAt = this.transform;
            m_CameraTransform = In_Camera.transform;
        }
    }
    private void Get_KeyInput()
    {
        if (m_bIsCrouch)
            return;

        m_vMoveVec = Vector3.zero;
        m_bIsRun = false;

        /*
        if (newGshake.isShake)
        {
            Debug.Log("Gshake");
            m_vMoveVec -= newGshake.moveVecR_q;
            
        }
        */

        Vector3 CamFoward = m_CameraTransform.forward;
        Vector3 vRight = Vector3.Cross(Vector3.up, CamFoward);
        CamFoward = Vector3.Cross(vRight, Vector3.up);

        if (Input.GetKey(KeyCode.W))
        {
            m_vMoveVec += CamFoward;
            m_fTotalSpeed = m_fSpeed;
            m_bIsRun = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_vMoveVec -= CamFoward;
            m_fTotalSpeed = m_fSpeed;
            m_bIsRun = true;

        }
        if (Input.GetKey(KeyCode.D))
        {
            m_vMoveVec += m_CameraTransform.right;
            m_fTotalSpeed = m_fSpeed;
            m_bIsRun = true;

        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_vMoveVec -= m_CameraTransform.right;
            m_fTotalSpeed = m_fSpeed;
            m_bIsRun = true;

        }

        m_vMoveVec = m_vMoveVec.normalized;
        Vector3 vPlayerRight = Vector3.Cross(Vector3.up, m_vMoveVec);
        m_vMoveVec = Vector3.Cross(vPlayerRight, Vector3.up).normalized;

        if (false == m_bIsJump && m_bIsGround == true)//점프하는 중 & 채공 중 & 미는 중이라면 run 애니메이션 재생 안되게
            m_Animator.SetBool("IsRun", m_bIsRun);



    }

    public void Player_Die()
    {//아 그냥 플레이어 멀티 스크립트 관련 게임 오브젝트 따로 파서 플레이어 child로 넣어줄껄
        m_Animator.SetTrigger("Death");
        m_Animator.SetBool("IsAlive", false);

        if (m_PV != null)
        {
            if (m_PV.IsMine)
                InGameUIMgr.Instance.Activate_RewpawnUI();
        }
    }


    private void Move_MultiMode()
    {
        if (m_PV.IsMine)
        {
            Move();

        }
    }

    private void Move_SingleMode()
    {
        Move();
    
    }

    private bool IsOnSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray, out slopeHit, 1f, groundlayer))
        {
            var angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            bool onSlope = angle != 0f && angle < maxSlopeAngle;
            //Debug.Log("On Slope : " + onSlope);
            return onSlope;
        }
        return false;
    }

    private void Move()
    {
        m_fTotalSpeed = 0f;

        Crouch();
        Get_KeyInput();
        Run();
        //transform.position += m_vMoveVec * m_fTotalSpeed * Time.deltaTime;
        transform.LookAt(transform.position + m_vMoveVec);
        //m_Rigidbody.AddForce(m_vMoveVec * m_fTotalSpeed, ForceMode.VelocityChange);
        
        //m_Rigidbody.AddForce(Physics.gravity);
        
        //m_Rigidbody.velocity = m_vMoveVec * m_fTotalSpeed;


        m_Rigidbody.MovePosition(transform.position + m_vMoveVec * m_fTotalSpeed * Time.deltaTime);
        
        //transform.Translate(m_vMoveVec * m_fTotalSpeed * Time.deltaTime);

        bool isOnslope = IsOnSlope();
        Vector3 gravity = isOnslope ? Vector3.zero : Physics.gravity;
        m_Rigidbody.velocity = (isOnslope ? Vector3.ProjectOnPlane(m_vMoveVec, slopeHit.normal).normalized : m_vMoveVec) * m_fTotalSpeed + gravity;
            
 
        if (m_bIsThrow) Item_aim();
 
        //PushLever();
        //Picking_Up();
 
        if(m_bJumpAvailable)
            Jump_Up();

        //    if (Mathf.Abs(m_Rigidbody.velocity.x) > m_fTotalSpeed)
        //    {
        //        m_Rigidbody.velocity = new Vector3(Mathf.Sign(m_Rigidbody.velocity.x) * m_fTotalSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
        //    }
        //    else if (Mathf.Abs(m_Rigidbody.velocity.z) > m_fTotalSpeed)
        //    {
        //        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, Mathf.Sign(m_Rigidbody.velocity.z) * m_fTotalSpeed);
        //    }
        //    else if (m_Rigidbody.velocity.y > m_fTotalSpeed)
        //    {
        //        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, Mathf.Sign(m_Rigidbody.velocity.y) * m_fTotalSpeed, m_Rigidbody.velocity.z);
        //    }
    }


    private void Run()
    {
        m_bIsSprint = false;
        if (!m_bIsCrouch && m_bIsRun && m_Status.Is_Usable())
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_bIsSprint = true;
                m_fTotalSpeed *= 1.5f;
                m_Status.Use_Stamina(25f);
            }
            if (false == m_bIsJump && m_bIsGround)
                m_Animator.SetBool("IsSprint", m_bIsSprint);
        }


    }

    private void Jump_Up()//바닥 ground와 충돌하면 down으로 이어지게. 지금은 임시로 jump 하나만 작동하도록
    {
        if (m_bHolding == false && !m_bIsCrouch && Input.GetKeyDown(KeyCode.Space))
        {
            if (m_bIsGround && !m_bIsJump && m_Status.Is_Usable())
            {
                m_bIsJump = true;
                m_bIsGround = false;
                m_Animator.SetBool("IsJump", m_bIsJump);
                m_Animator.SetTrigger("Jump");
                m_Animator.SetBool("IsGround", m_bIsGround);
                //m_Rigidbody.AddForce(Vector3.up * m_fJumpScale, ForceMode.Impulse);
                m_fJumpForce = m_fJumpScale;
            }
        }

        if (m_bIsJump == true && m_bIsGround == false)
        {//+: y+방향, -: y-방향
            m_fJumpForce = m_fJumpForce - Physics.gravity.magnitude * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + m_fJumpForce * Time.deltaTime, transform.position.z);
            
            //m_Rigidbody.AddForce(Vector3.up * m_fJumpForce, ForceMode.Impulse);

        }
    }
    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))//키가 뭐엿지
        {
            if (m_bIsGround && !m_bIsJump)
            {
                m_bIsCrouch = true;
                m_Animator.SetBool("IsCrouch", m_bIsCrouch);

            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_bIsCrouch = false;
            m_Animator.SetBool("IsCrouch", m_bIsCrouch);
        }

    }
    private void PushLever()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_Animator.SetTrigger("PushLever");
        }
    }

    /*
    private void Throw()
    {
        if (Input.GetKeyDown(KeyCode.F))
            m_Animator.SetTrigger("Throw");
    }
    */

    /*
    private void Picking_Up()
    {
        if (Input.GetKeyDown(KeyCode.C))
            m_Animator.SetTrigger("PickingUp");
    }
    */

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("stair"))
        {
            m_bIsJump = false;
            m_bIsGround = true;
            m_Animator.SetBool("IsJump", m_bIsJump);
            m_Animator.SetBool("IsGround", m_bIsGround);

            if (collision.gameObject.CompareTag("stair"))
            {
                m_Rigidbody.AddForce(transform.forward * m_fTotalSpeed * 0.4f, ForceMode.VelocityChange);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.CompareTag("stair"))
        {
            m_Rigidbody.AddForce(transform.forward * m_fTotalSpeed * 0.4f, ForceMode.VelocityChange);
        }

        else        */
        if (collision.gameObject.CompareTag("Obstacle"))//낙하물 충돌
        {
            float fDamage = collision.gameObject.GetComponent<FallingObject>().Get_Damage();
            m_Status.Set_Damage(fDamage);
        }

        else if (collision.gameObject.CompareTag("FallObjects"))
        {
            //if(SceneManager.GetActiveScene().name == "Apartment_Phase3")
            if (Cushion.instance.isUsing)
            {
                Cushion.instance.CushionDamage();
                m_Status.Set_Damage(2);
            }
            else
            {
                m_Status.Set_Damage(5);
            }
        }

        else if(collision.gameObject.CompareTag("Building"))
        {
            m_Status.Set_Damage(m_Status.Get_MaxHP());
        }

        //지하철 낙하물
        else if (collision.gameObject.CompareTag("Falling"))
        {
            float fDamage = collision.gameObject.GetComponent<FallingObject>().Get_Damage();
            m_Status.Set_Damage(fDamage);
        }

        //지하철 AI와 충돌 시 질서 게이지 감소
        if (collision.gameObject.CompareTag("AI"))
        {
            SubwayInventory.instance.orderGage.Cut_Order("AI");
        }

        
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("stair"))
        {//땅에 닿아서 착지 애니메이션으로 이동
            m_bIsJump = false;
            m_bIsGround = true;
            m_Animator.SetBool("IsJump", m_bIsJump);
            m_Animator.SetBool("IsGround", m_bIsGround);
            ///Debug.Log("on " + collision.gameObject.name);
        }
        
    }

    public void SetAnimation(string parameter, bool flag)
    {
        m_Animator.SetBool(parameter, flag);
    }
    public void SetAnimation_Trigger(string parameter)
    {
        m_Animator.SetTrigger(parameter);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndPoint"))
        {
            Player_GoalIn();
        }
        else if (other.gameObject.CompareTag("MiddleGoal"))
        {
            Player_NextPhase();
        }
        else if (other.gameObject.CompareTag("Interactive"))
        {
            Curr_InteractiveObj = other.gameObject.GetComponent<Interactive>();
            if (Curr_InteractiveObj.Get_Type() == (int)Interactive.INTERACT.LEVER)
                m_bInteract_Lever = true;
            //if (Curr_InteractiveObj.Get_Type() == (int)Interactive.INTERACT.COLUMN)
            //    m_bInteract_Column = true;
        }
        else if(other.gameObject.CompareTag("Train"))
        {
            this.gameObject.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactive"))
        {
            m_bInteract_Lever = false;
            //m_bInteract_Column = false;
            Curr_InteractiveObj = null;
        }
        else if (other.gameObject.CompareTag("Train"))
        {
            this.gameObject.transform.parent = null;
        }
    }

    public bool Is_MyPlayer()
    {
        return m_PV.IsMine;
    }

    public void Player_GoalIn()
    {
        if (m_PV == null)
            SingleGameMgr.Instance.Player_GoalIn();
        else
            GameMgr.Instance.Player_GoalIn(m_PV.IsMine);

    }
    public void Player_NextPhase()
    {
        if (m_PV.IsMine)
            GameMgr.Instance.Player_NextPhase();
    }

    public void Resume(Vector3 vResumePoint)//거점 부활 햇을 때
    {
        m_Animator.SetBool("IsAlive", true);
        m_Status.PV_Reset();
        transform.position = vResumePoint;
        //아이템 리셋은 X
    }

    /// <summary>
    /// 상점아이템 관련
    /// 즉부, 즉사, 무적, 포션, 아드레날린
    /// </summary>
    public void Immediate_Resume()//GameMgr에서 실행 하면 해당 함수 불러서 체력 등 다시 세팅
    {
        m_Animator.SetBool("IsAlive", true);
        m_Status.Resume_Immediate();
    }

    public void Immediate_Death()
    {//다른 플레이어가 호출하게 됨
        m_PV.RPC("Targeting_Death", m_PV.Owner);
    }

    public bool Apply_DeathItem()
    {
        if (!m_bAdrenaline)
        {
            m_Effect.Stop_Particle(PlayerEffect.Effect.Death);
            m_Status.Set_Damage(m_Status.Get_MaxHP());
            return true;
        }
        return false;
    }

    public void Recover_Potion()
    {
        m_Effect.Play_Particle(PlayerEffect.Effect.Recover);
        m_Status.Recover_HP(m_Status.Get_MaxHP());
    }
    //일정 시간동안 유지. true false로 껐다 키는 기능으로
    public void Adrenaline(bool OnAdrenaline)//일단 달리기 빨라지는걸로
    {
        m_PV.RPC("Setbool_Adrenaline", RpcTarget.All, OnAdrenaline);
    }

    public void Invincible(bool bInvincible)//무적 상태(그냥 상처만 안받는 상태인가?)
    {
        m_PV.RPC("Setbool_Invincible", RpcTarget.All, bInvincible);

    }

    /// <summary>
    /// 지하철 맵 파밍 아이템 관련
    /// </summary>
    
    public void HpPotion(float ratio)
    {
        m_Status.Recover_HP(m_Status.Get_MaxHP() * ratio);
        Debug.Log("recover HP : " + m_Status.Get_HP());
    }

    public void StaminaPotion(float ratio)
    {
        m_Status.Recover_Stamina_byItem(m_Status.Get_MaxStamina() * ratio);
        Debug.Log("recover Stamina : " + m_Status.Get_Stamina());
    }

    public void OrderPotion(float ratio)
    {
        SubwayInventory.instance.orderGage.Recover_Order(SubwayInventory.instance.orderGage.Get_MaxOrder() * ratio);
        Debug.Log("recover Order : " + SubwayInventory.instance.orderGage.Get_Order());
    }

    public void KnockDown(float ratio)
    {
        m_Status.Set_Damage(m_Status.Get_MaxHP() * ratio);
    }

    public void SlowDown(float slowSpeed)
    {
        //5초동안 속도 반감 
        m_fSpeed = slowSpeed;
    }


    /// <summary>
    /// 아이템 던지기 로직
    /// * Item_aim()
    ///  (1) tab & c로 아이템 선택하면 손에 구체 소환 - SubwayItem.Item_effect();
    ///  (2) 마우스 좌클릭 누른 상태로 조준 - player.Item_aim();
    ///  (3) 마우스 클릭 떼면 투척 - player.Item_throw();
    /// </summary>


    public float throwForce = 50f;

    
    public void Item_aim() //m_bisThrow true일 때만 호출
    {
        /// Raycast 조준 (마우스 클릭으로 투척 벡터 설정)
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        RaycastHit rayHit;
        float rayLength = 50f;
        //int floorMask = LayerMask.GetMask("Ground");
        Vector3 throwDir;
        LayerMask layer = LayerMask.NameToLayer("OtherPlayer");
        if (Physics.Raycast(ray, out rayHit, rayLength,layer))//layermask
        {
            Debug.DrawRay(this.transform.position, rayHit.point, Color.red);

            throwDir = rayHit.point - this.transform.position;
            Debug.Log("Aim Dir " + throwDir);
            SubwayInventory.instance.On_Targeted(true);
        }
        else //RaycastHit false면 그냥 플레이어 앞에 떨어짐
        {
            throwDir = transform.forward;
            Debug.Log("Aim fail");
            SubwayInventory.instance.On_Targeted(false);

        }

        /// 투척
        if (throwDir != null && Input.GetMouseButtonDown(0))
        {
            Item_throw(throwDir);
        }
    }

    
    public void Item_throw(Vector3 throwAngle)
    {
        /// (2) grabbed 아이템 호출 & 종속관계 분리
        GameObject grabbedItem = OnHand.transform.GetChild(0).gameObject;

        OnHand.transform.DetachChildren();
        grabbedItem.transform.position = new Vector3(transform.position.x, transform.position.y+1f, transform.position.z);
        Debug.LogError("Item Angle: " + throwAngle);
        /// (3)) 던지기
        throwAngle.y = 0.4f;

        grabbedItem.GetComponent<SubwayItem_IGrabbed>().Set_isThrown(true, throwAngle);
        m_Animator.SetTrigger("Throw");
        

        SubwayInventory.instance.Active_AimPoint(false);

        m_bIsThrow = false;
    }
    
    


    [PunRPC]
    void Setbool_Adrenaline(bool bAdrenaline)
    {
        m_bAdrenaline = bAdrenaline;
        if(m_bAdrenaline)
        {
            m_Effect.Play_Particle(PlayerEffect.Effect.Adrenaline);
        }
        else
        {
            m_Effect.Stop_Particle(PlayerEffect.Effect.Adrenaline);
        }
    }
    [PunRPC]
    void Setbool_Invincible(bool Adrenaline)
    {
        m_bInvincible = Adrenaline;
        if (m_bInvincible)
        {
            m_Effect.Play_Particle(PlayerEffect.Effect.Invincible);
        }
        else
        {
            m_Effect.Stop_Particle(PlayerEffect.Effect.Invincible);
        }
    }
    [PunRPC]
    void Targeting_Death()
    {
        //이펙트 시작
        m_Effect.Play_Particle(PlayerEffect.Effect.Death);
        GameMgr.Instance.Trigger_Death();
    }
}