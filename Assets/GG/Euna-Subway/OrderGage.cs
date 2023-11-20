using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGage : MonoBehaviour
{
    public float m_fMaxOrder = 100;
    public float m_fCOrder = 3;
    private float m_fOrder;

    private void Start()
    {
        m_fOrder = m_fMaxOrder;
    }

    public float Get_Order()
    {
        return m_fOrder;
    }
    public float Get_MaxOrder()
    {
        return m_fMaxOrder;
    }

    public void Recover_Order(float fOrder)
    {
   
        m_fOrder += fOrder;
        if (m_fOrder > m_fMaxOrder)
        {
            m_fOrder = m_fMaxOrder;
        }
    }

    public void Cut_Order(string param) //HoldingBar 관련
    {
        Debug.Log("Cut Order " + param);
        //m_fOrder = Mathf.Max(0f, m_fOrder - m_fCOrder * Time.deltaTime);
        if(param == "AI")
        {
            m_fOrder -= 5f;
        }
        if(param == "bar")
        {
            m_fOrder -= 0.05f;
        }
        
        if(m_fOrder <= 0f)
        {
            m_fOrder = 0f;
            Phase1Mgr.Instance.clearCondition[0] = false;
        }
        
    }

    public void Set_Order() //Emergency Lever 관련 -> 실패 시 1/4 로 Set 
    {
        m_fOrder = m_fMaxOrder * 0.25f;
        Phase1Mgr.Instance.clearCondition[2] = false;
    }


}
