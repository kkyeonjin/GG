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
    private string PlayerName = "HiHi";
    public string m_szPlayerPrefab = "Local_Player";

    //로비 위치 할당
    public GameObject[] SpawnPoint;
    private List<int> ListIsOccupied;
    private int iMyLobbyPosIndex;

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
        PhotonNetwork.LocalPlayer.NickName = PlayerName;//플레이어 이름 임의로
        JoinLobby();
    }

    private void JoinLobby() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby()
    {
        Debug.Log("로비접속완료");
        SceneManager.LoadScene("RoomCode");
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
        //모든 접속하는 플레이어에 대해서 실행
        Debug.Log("방 입장");
        PhotonNetwork.LoadLevel("MultiLobby");
        if(PhotonNetwork.IsMasterClient)
        {
            //ListIsOccupied ={ 0,1,2,3,4,5,6,7};//리스트로 하나 없애고 반납하면 push하기
        }
        
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
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
        //SceneManager.LoadScene("RoomCode");
        
    }
    public override void OnLeftRoom()
    {
        Debug.Log("방 나감");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("플레이어 나감!" + otherPlayer.NickName);
        photonView.RPC("ReturnOccupied_ToMaster", PhotonNetwork.MasterClient, iMyLobbyPosIndex);
        //대기실 스폰 지점 마스터에게 반납
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
    {
        Debug.Log("방장 바뀜!!");
        if (newMasterClient == PhotonNetwork.LocalPlayer)
            StartButton.SetActive(true);
    }
    //마스터 클라이언트가 다른 클라이언트 위치 지정
    //======================================================
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
    public void Initialize_Players_InMap(List<GameObject> StartPoints)
    {//얘는 게임 시작할 때 단체로 위치 배정
       
            Photon.Realtime.Player[] Playerlist = PhotonNetwork.PlayerListOthers;
            int iLength = Playerlist.Length;

            for (int i = 0; i < iLength; ++i)
            {
                int idx = Random.Range(0, StartPoints.Count);
                photonView.RPC("Load_LocalPlayer", Playerlist[i], StartPoints[idx].transform.position);
                StartPoints.RemoveAt(idx);
            } 
        
    }
    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint)
    {
        Instantiate_Player(StartPoint);
    }
    [PunRPC]
    void ReturnOccupied_ToMaster(int ReturnIndex)
    {
        //bIsOccupied[ReturnIndex] = false;//마스터클라이언트한테 반납햇을 때 인덱스 추가
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
