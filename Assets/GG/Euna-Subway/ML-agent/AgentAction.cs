using Unity.MLAgents;
using UnityEngine;

public class AgentAction : Agent
{
    /// <summary>
    /// Status ����
    /// </summary>
    private float HP;
    private float Stamina;

    private bool isUsable()
    {
        if (Stamina <= 0) return false;
        return true;
    }


    /// <summary>
    /// Action ����
    /// </summary>
    private Rigidbody agentRb; 

    //�̵� �ӵ� �� ������
    public float moveSpeed;
    public float rotateSpeed;
    private float totalSpeed;

    public float jumpScale;
    private float jumpForce;

    //���� bool ����
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