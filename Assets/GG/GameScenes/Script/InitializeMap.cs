using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InitializeMap : MonoBehaviour
{
    public List<GameObject> StartPoints;
    public bool bInGame = false;

    public PhotonView m_PV;

    //로비에서 사용할 함수들
    private List<int> PosAssignable;
    private int iSpawnPosIndex;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("이니셜라이징 맵!");

        //GameMgr.Instance.Set_PlayerPos();
        //GameMgr.Instance.Set_Camera();
        if (bInGame && PhotonNetwork.IsMasterClient == true)
        {
            int idx = Random.Range(0, StartPoints.Count);
            GameMgr.Instance.Load_LocalPlayer(StartPoints[idx].transform.position);
            StartPoints.RemoveAt(idx);
            //NetworkManager.Instance.Initialize_Players_InMap(StartPoints);
            Initialize_Players_InMap();
        }
        else if (bInGame == false)
        {//로비에서 사용할 함수
            PosAssignable = new List<int>();
            for (int i = 0; i < NetworkManager.m_iMaxPlayer; ++i)
                PosAssignable.Add(i);

            if (PhotonNetwork.IsMasterClient == true)
            {
                int RandomValue= Random.Range(0, PosAssignable.Count);
                iSpawnPosIndex = PosAssignable[RandomValue];

                GameMgr.Instance.Load_LocalPlayer(StartPoints[iSpawnPosIndex].transform.position);
                PosAssignable.RemoveAt(RandomValue);
            }
            else
            {
                m_PV.RPC("Assign_SpawnPosition", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer);
            }
        }
    }
    private void Update()
    {
       // Debug.Log(PosAssignable.ToArray());
    }

    public void Initialize_Players_InMap()
    {//얘는 게임 시작할 때 단체로 위치 배정

        Photon.Realtime.Player[] Playerlist = PhotonNetwork.PlayerListOthers;
        int iLength = Playerlist.Length;

        for (int i = 0; i < iLength; ++i)
        {
            int idx = Random.Range(0, StartPoints.Count);

            m_PV.RPC("Load_LocalPlayer", Playerlist[i], StartPoints[idx].transform.position, -1);
            StartPoints.RemoveAt(idx);
        }

    }
    public void Return_PosIndex()
    {
        PosAssignable.Add(iSpawnPosIndex);
        m_PV.RPC("Add_SpawnPosIndex", RpcTarget.Others, iSpawnPosIndex);
    }
    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint,int iSpawnIndex = -1)
    {
        Debug.Log("생성!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);

        if (iSpawnIndex > -1)
            iSpawnPosIndex = iSpawnIndex;
    }
    
    /// 일반 로비에서 사용할 함수들
    [PunRPC]
    void Assign_SpawnPosition(Photon.Realtime.Player newPlayer)//마스터 클라이언트가 다른 클라이언트로부터 부탁받으면 수행
    {
        int RandomValue = Random.Range(0, PosAssignable.Count);
        int PosIdx = PosAssignable[RandomValue];
        PosAssignable.RemoveAt(RandomValue);

        m_PV.RPC("Load_LocalPlayer", newPlayer, StartPoints[PosIdx].transform.position, PosIdx);
        m_PV.RPC("Remove_SpawnPosIndex", RpcTarget.Others, RandomValue);
    }
    [PunRPC]
    void Add_SpawnPosIndex(int iIndex)
    {
        PosAssignable.Add(iIndex);
    }
    [PunRPC]
    void Remove_SpawnPosIndex(int iIndex)
    {
        PosAssignable.RemoveAt(iIndex);
    }
}
