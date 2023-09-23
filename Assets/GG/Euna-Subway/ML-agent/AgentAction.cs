using Unity.MLAgents;
using UnityEngine;

public class AgentAction : Agent
{
    /// <summary>
    /// Status 관련
    /// </summary>
    private float HP;
    private float Stamina;

    private bool isUsable()
    {
        if (Stamina <= 0) return false;
        return true;
    }


    /// <summary>
    /// Action 관련
    /// </summary>
    private Rigidbody agentRb; 

    //이동 속도 및 스케일
    public float moveSpeed;
    public float rotateSpeed;
    private float totalSpeed;

    public float jumpScale;
    private float jumpForce;

    //상태 bool 변수
    private bool isRun = false;
    private bool isSprint = false;
    private bool isJump = false;
    private bool isGround = true;


    private void Start()
    {
        //agentRb = 
    }

    private void Move()
    {
        isSprint = false;
        if(isSprint && )
    }
}