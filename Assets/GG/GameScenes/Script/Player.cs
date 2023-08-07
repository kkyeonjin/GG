using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_fSpeed;
    public float m_fJumpScale = 100;
    //public EventUI m_ClearUI;
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

    private float m_fTotalSpeed;

    private CharacterStatus m_Status;
    public Transform m_CameraTransform;

    private delegate void MoveFunc();

    private MoveFunc m_Moving;

    void Start()
    {
        m_Status = GetComponentInChildren<CharacterStatus>();
        m_Rigidbody = GetComponent<Rigidbody>();

        if (m_PV != null)
        {
            if (m_PV.IsMine)
            {
                GameMgr.Instance.m_LocalPlayerObj = this.gameObject;
                GameMgr.Instance.m_LocalPlayer = this;
                GameMgr.Instance.Set_Camera();
            }
            m_Moving = new MoveFunc(Move_MultiMode);
            
        }
        else
        {
            m_Animator = GetComponentInChildren<Animator>();
            m_Moving = new MoveFunc(Move);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        m_Moving();

        Debug.Log("IsRun:" + m_bIsRun);
        Debug.Log("IsSprint:" + m_bIsSprint);
        Debug.Log("IsGround:" + m_bIsGround);
        Debug.Log("IsJump:" + m_bIsJump);
    }

    public void Change_Animator(Animator In_Animator)
    {
        m_Animator = In_Animator;
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
    private void Get_MouseMovement()
    {
        // m_fXRotate += -Input.GetAxis("Mouse Y") * Time.deltaTime * 0.1f;
        // m_fYRotate += Input.GetAxis("Mouse X") * Time.deltaTime * 0.1f;

        m_fXRotate = -Input.GetAxis("Mouse Y") * Time.deltaTime * m_fRotateSpeed;
        m_fYRotate = Input.GetAxis("Mouse X") * Time.deltaTime * m_fRotateSpeed;

        m_YTotalrot += m_fYRotate;
        m_XTotalRot += m_fXRotate;

        m_XTotalRot = Mathf.Clamp(m_XTotalRot, -90, 90);

        transform.eulerAngles = new Vector3(m_XTotalRot, m_YTotalrot, 0);
    }
    private void Get_KeyInput()
    {
        m_vMoveVec = Vector3.zero;
        m_bIsRun = false;
        if (Input.GetKey(KeyCode.W))
        {
            m_vMoveVec += m_CameraTransform.forward;
            m_fTotalSpeed = m_fSpeed;
            m_bIsRun = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_vMoveVec -= m_CameraTransform.forward;
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

        if(false == m_bIsJump && m_bIsGround == true)//점프하는 중 & 채공 중이라면 run 애니메이션 재생 안되게
            m_Animator.SetBool("IsRun", m_bIsRun);



    }
    public void Set_Dead()
    {
        m_Animator.SetTrigger("Death");
    }
    private void Move_MultiMode()
    {
        if (m_PV.IsMine)
        {
            Move();

            if (Input.GetKey(KeyCode.T))
            {
                m_Status.Set_Damage(1f);
            }
        }
    }
    private void Move()
    {
        m_fTotalSpeed = 0f;
       // Get_MouseMovement();
        Get_KeyInput();
        Run();
        //transform.position += m_vMoveVec * m_fTotalSpeed * Time.deltaTime;
        transform.LookAt(transform.position + m_vMoveVec);
        m_Rigidbody.AddForce(m_vMoveVec * m_fTotalSpeed);
        m_Rigidbody.AddForce(Physics.gravity);
        m_Rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);

        Jump_Up();

        if (Mathf.Abs(m_Rigidbody.velocity.x) > m_fTotalSpeed)
        {
            m_Rigidbody.velocity = new Vector3(Mathf.Sign(m_Rigidbody.velocity.x) * m_fTotalSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
        }
        else if (Mathf.Abs(m_Rigidbody.velocity.z) > m_fTotalSpeed)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, Mathf.Sign(m_Rigidbody.velocity.z) * m_fTotalSpeed);
        }
        else if (m_Rigidbody.velocity.y > m_fTotalSpeed)
        {
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, Mathf.Sign(m_Rigidbody.velocity.y) * m_fTotalSpeed, m_Rigidbody.velocity.z);
        }
    }

   
    private void Run()
    {
        m_bIsSprint = false;
        if (m_bIsRun && m_Status.Is_Usable())
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_bIsGround && !m_bIsJump && m_Status.Is_Usable())
            {
                m_bIsJump = true;
                m_bIsGround = false;
                m_Animator.SetBool("IsJump", m_bIsJump);
                m_Animator.SetTrigger("Jump");
                m_Animator.SetBool("IsGround", m_bIsGround);
            }
            m_Rigidbody.AddForce(Vector3.up * m_fJumpScale, ForceMode.Impulse);
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
            float fDamage = collision.gameObject.GetComponent<FallingObject>().Get_Damage();
            m_Status.Set_Damage(fDamage);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            //m_ClearUI.Activate_and_Over();
            Application.Quit();
        }
        if( collision.gameObject.CompareTag("Ground"))
        {//땅에 닿아서 착지 애니메이션으로 이동
            m_bIsJump = false;
            m_bIsGround = true;
            m_Animator.SetBool("IsJump", m_bIsJump);
            m_Animator.SetBool("IsGround",m_bIsGround);
            
        }
    }

}
