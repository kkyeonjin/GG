using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager m_Instance = null;

    public const int m_iMaxPlayer = 8;
    public string m_szPlayerPrefab = "Local_Player";

    public GameObject StartButton { get; set; }


    public TMP_InputField m_RoomCode, m_NickNameInput;

    void Awake()
    {
        var duplicated = FindObjectsOfType<NetworkManager>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else if (null == m_Instance)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public static NetworkManager Instance
    {
        get
        {
            if (null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(960, 600, false);

    }
    void Update()
    {
       // Debug.Log(PhotonNetwork.NetworkClientState.ToString());
    }

    public string Get_Playername()
    {
        return PhotonNetwork.LocalPlayer.NickName;
    }
    //////////////////////////////////////////////////////////////
    public void Set_StartButton(GameObject button)
    {
        StartButton = button;
    }
    public void Instantiate_Player(Vector3 vStartPoint)
    {
        PhotonNetwork.Instantiate(m_szPlayerPrefab, vStartPoint, Quaternion.identity);
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();

        //바로 서버 연결
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버접속완료");
        PhotonNetwork.LocalPlayer.NickName = InfoHandler.Instance.Get_Name();//플레이어 이름 임의로
        JoinLobby();
    }

    private void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby()
    {
        Debug.Log("로비접속완료");
        SceneManager.LoadScene("RoomCode");

        Debug.Log("OnJoinedLobby StartScreen");
        SceneManager.sceneLoaded += ScreenTransition.Instance.StartScreen;

    }




    public void CreateRoom(TMP_InputField In_RoomCode)
    {
        m_RoomCode = In_RoomCode;
        PhotonNetwork.CreateRoom(m_RoomCode.text, new RoomOptions { MaxPlayers = m_iMaxPlayer });
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성");
    }

    public void JoinRoom(TMP_InputField In_RoomCode)
    {
        m_RoomCode = In_RoomCode;
        PhotonNetwork.JoinRoom(m_RoomCode.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("방 입장");
        PhotonNetwork.LoadLevel("MultiLobby");
        //레벨을 로드 할 때 접속한 플레이어도 함께 불러옴, 그냥 loadscene하면 자기자신 들어오기 전 플레이어 못 불러옴
        
    }
    
    public void StartGame(string In_StageName )
    {
        PhotonNetwork.LoadLevel(In_StageName);
    }

    public void LeaveRoom()
    {
        Debug.Log("방 나가는중!");
        m_RoomCode = null;
        ChangeMasterClient();
        PhotonNetwork.LeaveRoom();//연결이 모두 끊기면 알아서 photonNetwork.LoadLevel로 한 씬 모두 벗어남
        SceneManager.LoadScene("RoomCode");
    }
    public override void OnLeftRoom()
    {
        Debug.Log("방 나감");
        //SceneManager.LoadScene("RoomCode");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("플레이어 나감!" + otherPlayer.NickName);
        if (PhotonNetwork.IsMasterClient)
            WaitingRoomMgr.Instance.Update_PlayerList();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("플레이어 들어옴!" + newPlayer.NickName + "ActiveNum : "+newPlayer.ActorNumber);
    }

    public void ChangeMasterClient()
    {
        if (true == PhotonNetwork.IsMasterClient)
        {
            if(PhotonNetwork.PlayerListOthers.Length >0)
                PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerListOthers[0]);//이 플레이어 리스트가 방에 있는 사람들 리스트인지 아니면 전체 접속자 리스트인지

        }
    }
    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {//대기실에서만 사용되는 함수이기 때문에
        Debug.Log("방장 바뀜!!");
        if (newMasterClient == PhotonNetwork.LocalPlayer)
        {
            StartButton.SetActive(true);
            WaitingRoomMgr.Instance.Change_MasterClient();
        }
    }
    //마스터 클라이언트가 다른 클라이언트 위치 지정
    //======================================================

    public void OnPlayerDisconnected(Photon.Realtime.Player player)
    {

    }
    public void LeaveLobby()
    {
        Debug.Log("로비 나가는 중");
        PhotonNetwork.LeaveLobby();
    }
    public override void OnLeftLobby()
    {
        Debug.Log("로비나감");
        Disconnect();
    }

    

    private void Disconnect() => PhotonNetwork.Disconnect();
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("연결끊김");
        SceneManager.LoadScene("MenuUI");
    }



    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("방입장실패");
        JoinLobby();
    }


    [ContextMenu("정보")]
    void Info()
    {
        if (PhotonNetwork.InRoom)
        {
            print("현재 방 이름 : " + PhotonNetwork.CurrentRoom.Name);
            print("현재 방 인원수 : " + PhotonNetwork.CurrentRoom.PlayerCount);
            print("현재 방 최대인원수 : " + PhotonNetwork.CurrentRoom.MaxPlayers);

            string playerStr = "방에 있는 플레이어 목록 : ";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) playerStr += PhotonNetwork.PlayerList[i].NickName + ", ";
            print(playerStr);
        }
        else
        {
            print("접속한 인원 수 : " + PhotonNetwork.CountOfPlayers);
            print("방 개수 : " + PhotonNetwork.CountOfRooms);
            print("모든 방에 있는 인원 수 : " + PhotonNetwork.CountOfPlayersInRooms);
            print("로비에 있는지? : " + PhotonNetwork.InLobby);
            print("연결됐는지? : " + PhotonNetwork.IsConnected);
        }
    }
}
