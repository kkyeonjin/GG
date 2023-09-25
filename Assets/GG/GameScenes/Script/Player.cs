using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

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
    private bool m_bIsGround = false;
    private bool m_bIsCrouch = false;

    private bool m_bInteract_Column= false;
    private bool m_bInteract_Lever = false;
    private bool m_bInteract_EnterCode = false;

    private Interactive Curr_InteractiveObj;

    private bool m_bHolding = false;

    private bool m_bAdrenaline = false;
    private bool m_bInvincible = false;

    private float m_fTotalSpeed;
    private float m_fJumpForce;

    private CharacterStatus m_Status;
    public Transform m_CameraTransform;

    private delegate void MoveFunc();
    private MoveFunc m_Moving;

    void Start()
    {
        m_Status = GetComponentInChildren<CharacterStatus>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();

        if (m_PV != null)
        {
            this.gameObject.layer = 16;
            if (m_PV.IsMine)
            {//멀티모드일때만 

                GameMgr.Instance.Set_LocalPlayer(this);
                GameMgr.Instance.Set_Camera();

                this.gameObject.layer = 17;
            }
            m_Moving = new MoveFunc(Move_MultiMode);

        }
        else
        {
            
            m_Moving = new MoveFunc(Move);
        }

    }

    // Update is called once per frame
    void Update()
    {
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

        if (m_bIsCrouch || m_bHolding)
            return;

        m_vMoveVec = Vector3.zero;
        m_bIsRun = false;

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
            if(m_PV.IsMine)
                InGameUIMgr.Instance.Activate_RewpawnUI();
        }
    }
    private void Move_MultiMode()
    {
        if (m_PV.IsMine)
        {
            Move();

            if (Input.GetKey(KeyCode.T))
            {
                if(!m_Status.is_Dead())
                    m_Status.Set_Damage(1f);
            }
        }
    }
    private void Move()
    {
        m_fTotalSpeed = 0f;

        Crouch();
        Get_KeyInput();
        Run();

        transform.LookAt(transform.position + m_vMoveVec);
        m_Rigidbody.MovePosition(transform.position + m_vMoveVec * m_fTotalSpeed * Time.deltaTime);

        Falling();
        Jump_Up();
        
        PushLever();
        Entering_Code();
        Holding();
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
        }
        if (false == m_bIsJump && m_bIsGround)
            m_Animator.SetBool("IsSprint", m_bIsSprint);
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
            m_fJumpForce = m_fJumpForce - Physics.gravity.magnitude*Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + m_fJumpForce * Time.deltaTime, transform.position.z);
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
    private void Entering_Code()
    {
        if(m_bInteract_EnterCode)
        {
            if(m_bIsGround && Input.GetKeyDown(KeyCode.E))
            {
                Curr_InteractiveObj.Interacting(this.gameObject);
                m_Animator.SetTrigger("EnteringCode");
            }
        }
    }
    private void PushLever()
    {
        if (m_bInteract_Lever)
        {
            if (m_bIsGround && Input.GetKeyDown(KeyCode.E))
            {
                Curr_InteractiveObj.Interacting(this.gameObject);
                m_Animator.SetTrigger("PushLever");
            }
        }
    }

    private void Holding()
    {
        if (m_bInteract_Column)
        {

            if (m_bIsGround && Input.GetKey(KeyCode.E))
            {
                if (!m_bIsCrouch && m_bIsGround && !m_bIsJump)
                {
                    Curr_InteractiveObj.Interacting(this.gameObject);
                    m_bHolding = true;
                    m_Animator.SetBool("Holding", m_bHolding);
                }
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                m_bHolding = false;
                m_Animator.SetBool("Holding", m_bHolding);
            }
        }
    }

    private void Falling()
    {
        if(m_bIsJump == false && m_bIsGround == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Physics.gravity.magnitude * Time.deltaTime*Time.deltaTime, transform.position.z);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("stair"))
        {
            m_Rigidbody.AddForce(transform.forward * m_fTotalSpeed * 0.4f, ForceMode.VelocityChange);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))//낙하물 충돌
        {
            if (m_bInvincible == false)
            {
                float fDamage = collision.gameObject.GetComponent<FallingObject>().Get_Damage();
                m_Status.Set_Damage(fDamage);
            }
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            m_ClearUI.Activate_and_Over();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {//땅에 닿아서 착지 애니메이션으로 이동
            m_bIsJump = false;
            m_bIsGround = true;
            //collision.gameObject
            if (m_Animator != null)
            {
                m_Animator.SetBool("IsJump", m_bIsJump);
                m_Animator.SetBool("IsGround", m_bIsGround);
            }
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {//땅에 닿아서 착지 애니메이션으로 이동
            m_bIsGround = true;
            m_Animator.SetBool("IsGround", m_bIsGround);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {//땅에 닿아서 착지 애니메이션으로 이동
            m_bIsGround = false;
            if (m_bIsJump == false)
            {
                m_Animator.SetTrigger("Falling");
                m_Animator.SetBool("IsGround", m_bIsGround);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EndPoint"))
        {
            Player_GoalIn();
        }
        else if(other.gameObject.CompareTag("MiddleGoal"))
        {
            Player_NextPhase();
        }
        else if(other.gameObject.CompareTag("Interactive"))
        {
            Curr_InteractiveObj = other.gameObject.GetComponent<Interactive>();
            if(Curr_InteractiveObj.Get_Type() == (int)Interactive.INTERACT.LEVER)
                m_bInteract_Lever = true;
            if (Curr_InteractiveObj.Get_Type() == (int)Interactive.INTERACT.COLUMN)
                m_bInteract_Column = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactive"))
        {
            m_bInteract_Lever = false;
            m_bInteract_Column = false;
            Curr_InteractiveObj = null;
        }
    }

    public bool Is_MyPlayer()
    {
        return m_PV.IsMine;
    }

    public void Player_GoalIn()
    {
        if(m_PV == null)
            SingleGameMgr.Instance.Player_GoalIn();
        else
            GameMgr.Instance.Player_GoalIn(m_PV.IsMine);

    }
    public void Player_NextPhase()
    {
        if(m_PV.IsMine)
            GameMgr.Instance.Player_NextPhase();
    }

    public void Resume(Vector3 vResumePoint)//거점 부활 햇을 때
    {
        m_Animator.SetBool("IsAlive",true);
        m_Status.PV_Reset();
        transform.position = vResumePoint;
        //아이템 리셋은 X
    }
    //아이템 관련
    //즉부, 즉사, 무적, 포션, 아드레날린
    public void Immediate_Resume()//GameMgr에서 실행 하면 해당 함수 불러서 체력 등 다시 세팅
    {
        m_Animator.SetBool("IsAlive", true);
        m_Status.Resume_Immediate();
    }

    public void Immediate_Death()
    {//다른 플레이어가 호출하게 됨
        m_PV.RPC("Targeting_Death", m_PV.Owner);
    }
    
    public void Apply_DeathItem()
    {
        if(!m_bAdrenaline)
        {
            m_Status.Set_Damage(m_Status.Get_MaxHP());
        }
    }

    public void Recover_Potion()
    {
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

    [PunRPC]
    void Setbool_Adrenaline(bool bAdrenaline)
    {
        m_bAdrenaline = bAdrenaline;
        
    }
    [PunRPC]
    void Setbool_Invincible(bool Adrenaline)
    {
        m_bInvincible = Adrenaline;
    }
    [PunRPC]
    void Targeting_Death()
    {

    }

}
