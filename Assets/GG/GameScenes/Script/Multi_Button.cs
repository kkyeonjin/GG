using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Multi_Button : MonoBehaviour
{
    public PhotonView m_PV;
    
    void Start()
    {
        NetworkManager.Instance.Set_StartButton(this.gameObject);
        if (false == PhotonNetwork.IsMasterClient)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == true)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
