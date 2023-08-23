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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Update_Participant(string Name, int Level, bool isEmpty,bool bMasterClient)
    {
        m_PV.RPC("Update_Slot", RpcTarget.All, Name, Level.ToString(), isEmpty,bMasterClient);
    }
    public void Vacate_Slot()
    {
        m_PV.RPC("Update_Slot", RpcTarget.All, "", "", false,false);

    }

    public void SetReady()
    {
        m_PV.RPC("Ready",RpcTarget.All);
    }

    [PunRPC]
    void Update_Slot(string Name, string Level, bool isEmpty,bool bMasterClient)
    {
        PlayerLevel.text = Level;
        PlayerName.text = Name;

        //MasterClient.gameObject.SetActive(bMasterClient);
        bIsEmpty = isEmpty;
        SlotBackground.gameObject.SetActive(!bIsEmpty);
    }
    [PunRPC]
    void Ready()
    {
        bIsReady = !bIsReady;
        //ReadyImage.SetActive(bIsReady);
    }
}
