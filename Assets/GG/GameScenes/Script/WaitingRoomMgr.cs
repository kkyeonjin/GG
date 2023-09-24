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
    private Dictionary<Photon.Realtime.Player, bool> PlayerCheckReady;

    public UIButton ExitButton;

    private List<int> PosAssignable;
    private int iSpawnPosIndex;

    private int iListSlotIndex = -1;

    void Awake()
    {

        var duplicated = FindObjectsOfType<WaitingRoomMgr>();

        if (duplicated.Length > 1)
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
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
        PlayerCheckReady = new Dictionary<Photon.Realtime.Player, bool>();

        PosAssignable = new List<int>();
        for (int i = 0; i < NetworkManager.m_iMaxPlayer; ++i)
            PosAssignable.Add(i);

        m_PV.RPC("Assign_SpawnPosition", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer,InfoHandler.Instance.Get_Level());       
    }

    public void Return_PosIndex()//�÷��̾ ����
    {
        if (PlayerList.Count > 1)
        {
            //�÷��̾ ���� ���� �Ŀ� �ش� rpc ����
            m_PV.RPC("Player_Exit", RpcTarget.Others, iSpawnPosIndex, PhotonNetwork.LocalPlayer);
        }

        ExitButton.Exit_Room();
    }
   
    public void Change_MasterClient()//������ �ٲ���� ��
    {
        Debug.Log("�ٲ� �Ҹ�!");
        Update_PlayerList();
    }

    public void Set_Ready()
    {
        WaitingList[iListSlotIndex].SetReady();
        m_PV.RPC("PlayerReady", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    void Assign_SpawnPosition(Photon.Realtime.Player newPlayer, int Level)//������ Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ�κ��� ��Ź������ ����
    {
        int RandomValue = Random.Range(0, PosAssignable.Count);
        int PosIdx = PosAssignable[RandomValue];

        m_PV.RPC("Load_LocalPlayer", newPlayer, StartPoints[PosIdx].transform.position, PosIdx);
        m_PV.RPC("Player_Join", RpcTarget.All, RandomValue,newPlayer,Level);

        Update_PlayerList();

    }
    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint, int iSpawnIndex = -1)
    {
        Debug.Log("����!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);


        if (iSpawnIndex > -1)
            iSpawnPosIndex = iSpawnIndex;
    }
   
    [PunRPC]
    void Player_Join(int iIndex,Photon.Realtime.Player newPlayer, int Level)//������ ���� ��
    {
        PlayerList.Add(newPlayer, Level);
        PlayerCheckReady.Add(newPlayer, false);

        PosAssignable.RemoveAt(iIndex);
    }
    [PunRPC]
    public void Player_Exit(int iIndex, Photon.Realtime.Player ExitPlayer)//������ ���� ��
    {
        PosAssignable.Add(iIndex);
        PlayerList.Remove(ExitPlayer);
        PlayerCheckReady.Remove(ExitPlayer);

        Debug.Log("���� �� �ο�: " + PlayerList.Count);
    }

    [PunRPC]
    public void Update_PlayerList()//���常 �����ϸ� ��
    {

        int num = PlayerList.Count;
        Debug.Log(num);
        if (num < 1)
            return;

        int iIndex = 0;
        Photon.Realtime.Player MasterClient = PhotonNetwork.MasterClient;
        
        foreach(Photon.Realtime.Player Key in PlayerList.Keys)
        {
            string name = Key.NickName;
            int level = PlayerList[Key];
            WaitingList[iIndex].Update_Participant(name, level, false, (MasterClient == Key), PlayerCheckReady[Key]);
            m_PV.RPC("Get_ListSlot_Index", Key, iIndex++);
        }
        for (int i = iIndex; i < 8; ++i)
            WaitingList[i].Vacate_Slot();
    }
    [PunRPC]
    void PlayerReady(Photon.Realtime.Player ReadyPlayer)
    {
        bool State = PlayerCheckReady[ReadyPlayer];
        PlayerCheckReady[ReadyPlayer] = !State;
    }
    [PunRPC]
    void Get_ListSlot_Index(int iIndex)//ready ��ư �����ؾ��ؼ�
    {
        iListSlotIndex = iIndex;
    }
}
