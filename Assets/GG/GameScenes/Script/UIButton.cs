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

    private Image ButtonImage;
    private bool m_bIsSelected = false;

    void Start()
    {
        if (m_iCharacterIndex > -1)
        {
            ButtonImage = MyButton.image;
            if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
                MyButton.interactable = false;
            else if (InfoHandler.Instance.Get_CurrCharacter() == m_iCharacterIndex)
            {
                m_bIsSelected = true;
            }
            Avatar_Selected(m_bIsSelected);
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
            ButtonImage.color = new Color(0.75f, 0.75f, 0.75f);
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
        Debug.Log("Multi Playing Mode");
        NetworkManager.Instance.ConnectToServer();
    }
    public void Lobby_Single()
    {
        Debug.Log("Single Playing Mode");
        SceneManager.LoadScene("Lobby");
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
        SceneManager.LoadScene("Store");
    }

    public void MyRoom()
    {
        SceneManager.LoadScene("MyRoom");
    }

    public void Backto_Menu()
    {
        SceneManager.LoadScene("MenuUI");
        InfoHandler.Instance.Clear_HoldingItem();
    }

    ///multi///
    public void Exit_Room()
    {
        NetworkManager.Instance.LeaveRoom();
        GameMgr.Instance.Destroy_Player();
        GameMgr.Instance.Destroy_Myself();
        InfoHandler.Instance.Clear_HoldingItem();
    }

    public void Exit_RoomCode()
    {
        Debug.Log("Exit_Multi");
        NetworkManager.Instance.LeaveLobby();
    }
 
    public void Multi_EnterCode()
    {
        NetworkManager.Instance.JoinRoom(m_RoomCode);
    }

    public void Multi_CreateRoom()
    {//나중에 초대 코드로
        NetworkManager.Instance.CreateRoom(m_RoomCode);
    }

    public void Multi_ExitGame()
    {
        Debug.Log("Multi_ExitGame");
        SceneManager.LoadScene("MultiLobby");
    }

   
}
