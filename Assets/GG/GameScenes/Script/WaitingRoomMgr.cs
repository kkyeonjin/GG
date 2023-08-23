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

    public void Return_PosIndex()//�÷��̾ ����
    {
        PosAssignable.Add(iSpawnPosIndex);
        m_PV.RPC("Player_Exit", RpcTarget.Others, iSpawnPosIndex,PhotonNetwork.LocalPlayer);
        m_PV.RPC("Update_PlayerList", RpcTarget.MasterClient);
    }

    public void Change_MasterClient()//������ �ٲ���� ��
    {
        Debug.Log("�ٲ� �Ҹ�!");
        Update_PlayerList();
    }

    [PunRPC]
    void Assign_SpawnPosition(Photon.Realtime.Player newPlayer, int Level)//������ Ŭ���̾�Ʈ�� �ٸ� Ŭ���̾�Ʈ�κ��� ��Ź������ ����
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
        Debug.Log("����!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);


        if (iSpawnIndex > -1)
            iSpawnPosIndex = iSpawnIndex;
    }
    [PunRPC]
    void Player_Exit(int iIndex, Photon.Realtime.Player ExitPlayer)//������ ���� ��
    {
        PosAssignable.Add(iIndex);
        PlayerList.Remove(ExitPlayer);
        Debug.Log("���� �� �ο�: " + PlayerList.Count);
    }
    [PunRPC]
    void Player_Join(int iIndex,Photon.Realtime.Player newPlayer, int Level)//������ ���� ��
    {
        PosAssignable.RemoveAt(iIndex);
        PlayerList.Add(newPlayer, Level);
    }

    [PunRPC]
    void Update_PlayerList()//���常 �����ϸ� ��
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
