using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AIAvatarSet : MonoBehaviour
{
    public GameObject[] m_arrAvatar;
    public CompetitorAgent m_OwnerObject;

    public PhotonView m_PV;  

    void Start()
    {
        int idx = Random.Range(0, 8);
        m_PV.RPC("Changing", RpcTarget.All, idx);
        
    }

    void Update()
    {

    }

    [PunRPC]
    void Changing(int iIndex)
    {
        m_arrAvatar[iIndex].SetActive(true);
        m_OwnerObject.Set_Animator(m_arrAvatar[iIndex].GetComponent<Animator>());
    }

}
