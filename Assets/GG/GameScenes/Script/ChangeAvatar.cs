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
    private GameObject m_InactiveAvatar;

    public PhotonView m_PV;  
    public Player m_OwnPlayer;

    public void Change_Avatar()
    {//변경한 아바타가 모두에게 반영 되어야 함
        m_PV.RPC("Changing", RpcTarget.All);
    }

    void Start()
    {
        m_ActiveAvatar = m_Avatar1;
        m_InactiveAvatar = m_Avatar2;

        m_Avatar2.SetActive(false);
        m_Avatar3.SetActive(false);
        m_Avatar4.SetActive(false);
        m_Avatar5.SetActive(false);
        m_Avatar6.SetActive(false);
        m_Avatar7.SetActive(false);
        m_Avatar8.SetActive(false);
        m_Avatar9.SetActive(false);

        m_OwnPlayer.Change_Animator(m_ActiveAvatar.GetComponent<Animator>());

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
    }

    void Update()
    {

    }

    [PunRPC]
    void Changing()
    {
        GameObject TempObj = m_ActiveAvatar;
        m_ActiveAvatar = m_InactiveAvatar;
        m_InactiveAvatar = TempObj;

        m_InactiveAvatar.SetActive(false);
        m_ActiveAvatar.SetActive(true);

        m_OwnPlayer.Change_Animator(m_ActiveAvatar.GetComponent<Animator>());
    }

}
