using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeAvatar : MonoBehaviour
{
    public GameObject m_Avatar1;
    public GameObject m_Avatar2;
    public GameObject m_Avatar3;
    public GameObject m_Avatar4;
    public GameObject m_Avatar5;
    public GameObject m_Avatar6;
    public GameObject m_Avatar7;
    public GameObject m_Avatar8; 
    public GameObject m_Avatar9;

    private GameObject[] m_arrAvatar;

    private GameObject m_ActiveAvatar;
    

    public PhotonView m_PV;  
    public Player m_OwnPlayer;

    public void Change_Avatar(int iIndex)
    {//변경한 아바타가 모두에게 반영 되어야 함
        m_PV.RPC("Changing", RpcTarget.All, iIndex);
    }

    void Start()
    {
        m_arrAvatar = new GameObject[9];
        m_arrAvatar[0] = m_Avatar1;
        m_arrAvatar[1] = m_Avatar2;
        m_arrAvatar[2] = m_Avatar3;
        m_arrAvatar[3] = m_Avatar4;
        m_arrAvatar[4] = m_Avatar5;
        m_arrAvatar[5] = m_Avatar6;
        m_arrAvatar[6] = m_Avatar7;
        m_arrAvatar[7] = m_Avatar8;
        m_arrAvatar[8] = m_Avatar9;

        int CurrIndex = InfoHandler.Instance.Get_CurrCharacter();

        
        m_ActiveAvatar = m_arrAvatar[0];
        m_ActiveAvatar.SetActive(true);
        for(int i=1;i<9;++i)
        {
            m_arrAvatar[i].SetActive(false);
        }
        if(m_PV != null)
            Change_Avatar(CurrIndex);
        else
        {
            m_ActiveAvatar.SetActive(false);
            m_ActiveAvatar = m_arrAvatar[CurrIndex];
            m_ActiveAvatar.SetActive(true);

            InfoHandler.Instance.Set_CurrCharacter(CurrIndex);

            m_OwnPlayer.Change_Animator(m_ActiveAvatar.GetComponent<Animator>());
        }
    }

    void Update()
    {

    }

    [PunRPC]
    void Changing(int iIndex)
    {
        m_ActiveAvatar.SetActive(false);
        m_ActiveAvatar = m_arrAvatar[iIndex];
        m_ActiveAvatar.SetActive(true);

        InfoHandler.Instance.Set_CurrCharacter(iIndex);

        m_OwnPlayer.Change_Animator(m_ActiveAvatar.GetComponent<Animator>());
    }

}
