using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField m_RoomCode;
    public int m_iCharacterIndex=-1;
    
    public Button MyButton;
    public GameObject m_ConnectedUI;

    public ScreenTransition TransitionImg;
    public Image ButtonImage;

    private bool m_bIsSelected = false;

    public 

    void Start()
    {
        TransitionImg = ScreenTransition.Instance;

        if (m_iCharacterIndex > -1)
        {
            //ButtonImage = MyButton.image;
            if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
            {
                MyButton.interactable = false;
                ButtonImage.color = new Color(0.9f, 0.9f, 0.9f);
            }
            else
            {
                Color color = MyButton.image.color;
                color.a = 0f;
                MyButton.image.color = color;
                if (InfoHandler.Instance.Get_CurrCharacter() == m_iCharacterIndex)
                {
                    m_bIsSelected = true;
                }
                Avatar_Selected(m_bIsSelected);
            }
        }
    }

    private void Update()
    {
        if(m_iCharacterIndex > -1)
        {
            if(m_bIsSelected && (InfoHandler.Instance.Get_CurrCharacter() != m_iCharacterIndex))
            {
                Avatar_Selected(false);
            }
        }

    }

    //lobby에서 사용하는 characterselect용
    public void Avatar_Selected(bool bInput)
    {
        m_bIsSelected = bInput;
        if (m_bIsSelected)
        {
            ButtonImage.color = new Color(1f, 1f, 1f);
        }
        else
        {
            ButtonImage.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
    public void Change_Avatar()
    {
        GameMgr.Instance.Change_Avatar(m_iCharacterIndex);
        Avatar_Selected(true);
    }
    public void Change_Avatar_Single()
    {
        InfoHandler.Instance.Set_CurrCharacter(m_iCharacterIndex);
        Avatar_Selected(true);
    }

    /// //////////////////////////////////////////////////
    /// </summary>
    public void Lobby_Multi()
    {
        TransitionImg.EndScreen();
        Invoke("MultiLobby", TransitionImg.Get_TransitionTime());
    }
    public void Lobby_Single()
    {
        TransitionImg.EndScreen();
        Invoke("SingleLobby", TransitionImg.Get_TransitionTime());
    }

    void SingleLobby()
    {
        Debug.Log("Single Playing Mode");
        SceneManager.LoadScene("Lobby");
        SceneManager.sceneLoaded += TransitionImg.StartScreen;
    }
    void MultiLobby()
    {
        Debug.Log("Multi Playing Mode");
        NetworkManager.Instance.ConnectToServer();

    }
    public void Exit_Game()
    {
        Debug.Log("Exit");

        Application.Quit();
    }

    public void Activate_ConnectedUI()
    {
        bool bisActivate = m_ConnectedUI.activeInHierarchy;
        m_ConnectedUI.SetActive(!bisActivate);
    }

    public void Store()
    {
        TransitionImg.EndScreen();
        Invoke("LoadStore", TransitionImg.Get_TransitionTime());
    }
    void LoadStore()
    {
        SceneManager.LoadScene("Store");
        SceneManager.sceneLoaded += TransitionImg.StartScreen;

    }
    public void MyRoom()
    {
        TransitionImg.EndScreen();
        Invoke("LoadMyRoom", TransitionImg.Get_TransitionTime());
    }
    void LoadMyRoom()
    {
        SceneManager.LoadScene("MyRoom");
        SceneManager.sceneLoaded += TransitionImg.StartScreen;

    }
    public void Backto_Menu()
    {
        TransitionImg.EndScreen();
        Invoke("LoadMenu", TransitionImg.Get_TransitionTime());
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("MenuUI");
        SceneManager.sceneLoaded += TransitionImg.StartScreen;
    }

    ///multi///
    public void Exit_Room()
    {
        TransitionImg.EndScreen();
        Invoke("ExitRoom", TransitionImg.Get_TransitionTime());
    }

    void ExitRoom()
    {
        //InfoHandler.Instance.Clear_HoldingItem();
        NetworkManager.Instance.LeaveRoom();
        
        //GameMgr.Instance.Destroy_Player(); // NetwordManager에서 방 나갈때 autoCleanUp으로 player를 삭제, 전에 player 삭제하면 문제 생겨서 방 못나감
    }

    public void Exit_RoomCode()
    {
        TransitionImg.EndScreen();
        Invoke("ExitRoomCode", TransitionImg.Get_TransitionTime());
    }
    void ExitRoomCode()
    {
        Debug.Log("Exit_Multi");
        NetworkManager.Instance.LeaveLobby();
        Debug.Log("ExitRoomCode StartScreen");
        SceneManager.sceneLoaded += TransitionImg.StartScreen;
    }


    public void Multi_EnterCode()
    {
        NetworkManager.Instance.JoinRoom(m_RoomCode);

    }

    public void Multi_CreateRoom()
    {//나중에 초대 코드로
        NetworkManager.Instance.CreateRoom(m_RoomCode);
    }
   
}