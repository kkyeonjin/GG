using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WaitingRoomMgr : MonoBehaviour
{

    public static WaitingRoomMgr m_Instance;

    public PhotonView m_PV;
    public List<GameObject> StartPoints;

    public List<ParticipantInfo> WaitingList;

    private Dictionary<Photon.Realtime.Player,int> PlayerList;
    private List<int> PosAssignable;
    private int iSpawnPosIndex;

    void Awake()
    {

        var duplicated = FindObjectsOfType<WaitingRoomMgr>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }


    }

    public static WaitingRoomMgr Instance
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
        PlayerList = new Dictionary<Photon.Realtime.Player, int>();

        PosAssignable = new List<int>();
        for (int i = 0; i < NetworkManager.m_iMaxPlayer; ++i)
            PosAssignable.Add(i);

        //if (PhotonNetwork.IsMasterClient == true)
        //{
        //    int RandomValue = Random.Range(0, PosAssignable.Count);
        //    iSpawnPosIndex = PosAssignable[RandomValue];

        //    GameMgr.Instance.Load_LocalPlayer(StartPoints[iSpawnPosIndex].transform.position);
        //    PosAssignable.RemoveAt(RandomValue);

        //    PlayerList.Add(PhotonNetwork.LocalPlayer, InfoHandler.Instance.Get_Level());
        //    Update_PlayerList();
        //}
        //else
        //{
            m_PV.RPC("Assign_SpawnPosition", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer,InfoHandler.Instance.Get_Level());
        //}
    }

    public void Return_PosIndex()//플레이어가 나감
    {
        PosAssignable.Add(iSpawnPosIndex);
        m_PV.RPC("Player_Exit", RpcTarget.Others, iSpawnPosIndex,PhotonNetwork.LocalPlayer);
        m_PV.RPC("Update_PlayerList", RpcTarget.MasterClient);
    }

    public void Change_MasterClient()//방장이 바뀌었을 때
    {
        Debug.Log("바뀌어서 불림!");
        Update_PlayerList();
    }

    [PunRPC]
    void Assign_SpawnPosition(Photon.Realtime.Player newPlayer, int Level)//마스터 클라이언트가 다른 클라이언트로부터 부탁받으면 수행
    {
        int RandomValue = Random.Range(0, PosAssignable.Count);
        int PosIdx = PosAssignable[RandomValue];
        PosAssignable.RemoveAt(RandomValue);

        m_PV.RPC("Load_LocalPlayer", newPlayer, StartPoints[PosIdx].transform.position, PosIdx);
        m_PV.RPC("Player_Join", RpcTarget.Others, RandomValue,Level);

        Update_PlayerList();

    }
    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint, int iSpawnIndex = -1)
    {
        Debug.Log("생성!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);


        if (iSpawnIndex > -1)
            iSpawnPosIndex = iSpawnIndex;
    }
    [PunRPC]
    void Player_Exit(int iIndex, Photon.Realtime.Player ExitPlayer)//참가자 나갈 때
    {
        PosAssignable.Add(iIndex);
        PlayerList.Remove(ExitPlayer);
        Debug.Log("대기실 총 인원: " + PlayerList.Count);
    }
    [PunRPC]
    void Player_Join(int iIndex,Photon.Realtime.Player newPlayer, int Level)//참가자 들어올 때
    {
        PosAssignable.RemoveAt(iIndex);
        PlayerList.Add(newPlayer, Level);
    }

    [PunRPC]
    void Update_PlayerList()//방장만 정리하면 됨
    {

        int num = PlayerList.Count;
        Debug.Log(num);
        if (num < 1)
            return;

        int iIndex = 0;

        foreach(Photon.Realtime.Player Key in PlayerList.Keys)
        {
            string name = Key.NickName;
            int level = PlayerList[Key];
            WaitingList[iIndex++].Update_Participant(name, level, false, false);
        }
        for (int i = iIndex; i < 8; ++i)
            WaitingList[i].Vacate_Slot();
    }
}
