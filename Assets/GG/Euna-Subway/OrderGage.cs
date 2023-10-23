using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGage : MonoBehaviour
{
    public PhotonView m_PV;

    public float m_fMaxOrder = 100;
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
