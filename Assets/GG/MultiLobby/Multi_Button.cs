using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Multi_Button : MonoBehaviour
{
    public Button MyButton;
   
    public bool IsstartButton;
    public bool OnlyMasterClient;
    public bool IsSelectStage;
    void Start()
    {
        if(IsstartButton)
            NetworkManager.Instance.Set_StartButton(this.gameObject);

        bool IsMaster = PhotonNetwork.IsMasterClient;

        if (OnlyMasterClient)
        {
            this.gameObject.SetActive(IsMaster);
        }
        else if (IsSelectStage)
            MyButton.interactable = false;
        else
            this.gameObject.SetActive(!IsMaster);

    }

    // Update is called once per frame
    void Update()
    {
        bool IsMaster = PhotonNetwork.IsMasterClient;
        if (IsMaster)
        {
            if (OnlyMasterClient)
            {
                this.gameObject.SetActive(IsMaster);
            }
            else if (IsSelectStage)
                MyButton.interactable = true;
            else
                this.gameObject.SetActive(!IsMaster);
        }
    }
}
