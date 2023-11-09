using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class SelectStageUI : MonoBehaviour
{
    public PhotonView m_PV;

    public TextMeshProUGUI SelectedStage;
    public Image mapImage;
    public Sprite subway, apart;
    public SelectStageUI SelectingButton;
    // Start is called before the first frame update

    private int m_iStageIndex = 0;

    void Start()
    {
       
    }

    public void Multi_StartGame()
    {
        //NetworkManager.Instance.StartGame("Multi_Subway");
        string SceneName = "";
        //m_iStageIndex = SelectingButton.Get_Index();
        switch (0)
        {
            case 0:
                SceneName = "Multi_Subway";
                break;
        }
        m_PV.RPC("StartGame", RpcTarget.All, SceneName);
    }

    public void Game_Start()//single mode
    {
        SceneManager.LoadScene(SelectedStage.text);
    }

    public void Exit_Stage()//싱글모드
    {
        SceneManager.LoadScene("Lobby");
    }

    public void Multi_ExitStage()//멀티 스테이지 나가기
    {
        m_PV.RPC("BacktoLobby", RpcTarget.All);
    }

    public void Change_Stage()
    {
        m_iStageIndex = (m_iStageIndex + 1) % 2;
        switch(m_iStageIndex)
        {
            case 0:
                SelectedStage.text = "Apartment";//잠깐 멀티로
                mapImage.sprite = subway;

                break;
        }
        //SelectedStage.text = sz_SelectedStage;
    }
    public void ChangeStage_Multi()
    {
        m_iStageIndex = (m_iStageIndex + 1) % 1;
        switch (m_iStageIndex)
        {
            case 0:
                SelectedStage.text = "Subway";//잠깐 멀티로
               // mapImage.sprite = subway;

                break;
        }
    }
    public int Get_Index()
    {
        return m_iStageIndex;
    }
    //멀티 게임 임시
    [PunRPC]
    void StartGame(string SceneName)
    {
        
        NetworkManager.Instance.StartGame(SceneName);
    }
    [PunRPC]
    void BacktoLobby()
    {
        NetworkManager.Instance.StartGame("MultiLobby");
    }


}
