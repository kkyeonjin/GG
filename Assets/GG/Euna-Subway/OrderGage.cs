using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGage : MonoBehaviour
{
    public PhotonView m_PV;

    public float m_fMaxOrder = 100;
    public float m_fCOrder = 3;
    private float m_fOrder;

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
        if (m_PV != null)
        {
            m_PV.RPC("Update_Order", RpcTarget.All, fOrder);
        }
        m_fOrder += fOrder;
        if (m_fOrder > m_fMaxOrder)
        {
            m_fOrder = m_fMaxOrder;
        }
    }

    public void Cut_Order() //HoldingBar 관련
    {
        Debug.Log("Cut Order");
        m_fOrder = Mathf.Max(0f, m_fOrder - m_fCOrder * Time.deltaTime);
        /*
        if(m_fOrder <= 0f)
        {
            Phase1Mgr.Instance.clearCondition[0] = false;
        }
        */
    }

    public void Set_Order() //Emergency Lever 관련 -> 실패 시 1/4 로 Set 
    {
        m_fOrder = m_fMaxOrder * 0.25f;
        Phase1Mgr.Instance.clearCondition[2] = false;
    }

    [PunRPC]
    void Update_Order(float fOrder)
    {
        m_fOrder += fOrder;
        if (m_fOrder > m_fMaxOrder)
        {
            m_fOrder = m_fMaxOrder;
        }
    }
}
