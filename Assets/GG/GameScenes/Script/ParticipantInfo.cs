using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class ParticipantInfo : MonoBehaviour
{//대기실 참가자 리스트에 사용, 변화를 포톤 뷰로 반영

    public PhotonView m_PV;

    public TextMeshProUGUI PlayerLevel;
    public TextMeshProUGUI PlayerName;
    public Image MasterClient;
    public Image SlotBackground;
    public Image ReadyImage;

    private bool bIsEmpty = true;
    private bool bIsReady = false;
    // Start is called before the first frame update
    void Start()
    {
        Color Temp = MasterClient.color; Temp.a = 0f;
        MasterClient.color = Temp;

        Temp = SlotBackground.color; Temp.a = 0f;
        SlotBackground.color = Temp;

        Temp = ReadyImage.color; Temp.a = 0f;
        ReadyImage.color = Temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Participant(string Name, int Level, bool isEmpty,bool bMasterClient, bool IsReady)
    {
        m_PV.RPC("Update_Slot", RpcTarget.All, Name, Level.ToString(), isEmpty,bMasterClient, IsReady);
    }
    public void Vacate_Slot()
    {
        m_PV.RPC("Update_Slot", RpcTarget.All, "", "", true,false,false);


    }

    public void SetReady()
    {
        m_PV.RPC("Ready",RpcTarget.All);
    }

    [PunRPC]
    void Update_Slot(string Name, string Level, bool isEmpty, bool bMasterClient,bool isReady)
    {
        PlayerLevel.text = Level;
        PlayerName.text = Name;
        Color Temp;

        bIsEmpty = isEmpty;
        if (bIsEmpty)
        {
            Temp = SlotBackground.color; Temp.a = 0f;
            SlotBackground.color = Temp;
            Temp = MasterClient.color; Temp.a = 0f;
            MasterClient.color = Temp;
            Temp = ReadyImage.color; Temp.a = 0f;
            ReadyImage.color = Temp;
        }
        else
        {
            Temp = SlotBackground.color; Temp.a = 0.5f;
            SlotBackground.color = Temp;

            if (bMasterClient == true)
            {
                Temp = MasterClient.color; Temp.a = 1f;
                MasterClient.color = Temp;
                Temp = ReadyImage.color; Temp.a = 0f;
                ReadyImage.color = Temp;

            }
            else
            {
                bIsReady = isReady;

                if (bIsReady)
                {
                    Temp = ReadyImage.color; Temp.a = 1f;
                }
                else
                {
                    Temp = ReadyImage.color; Temp.a = 0f;
                }
                ReadyImage.color = Temp;
            }
        }
          
    }
    [PunRPC]
    void Ready()
    {
        bIsReady = !bIsReady;
        Color Temp;
        if (bIsReady)
        {
            Temp = ReadyImage.color; Temp.a = 1f;
        }
        else
        {
            Temp = ReadyImage.color; Temp.a = 0f;
        }
        ReadyImage.color = Temp;

    }
}
