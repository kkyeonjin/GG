using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InitializeMap : MonoBehaviour
{
    public List<GameObject> StartPoints;
    public bool bInGame = false;

    public PhotonView m_PV;
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
        else if( bInGame == false)
        {
            int idx = Random.Range(0, StartPoints.Count);
            GameMgr.Instance.Load_LocalPlayer(StartPoints[idx].transform.position);
        }
    }

    public void Initialize_Players_InMap()
    {//얘는 게임 시작할 때 단체로 위치 배정

        Photon.Realtime.Player[] Playerlist = PhotonNetwork.PlayerListOthers;
        int iLength = Playerlist.Length;

        for (int i = 0; i < iLength; ++i)
        {
            int idx = Random.Range(0, StartPoints.Count);

            m_PV.RPC("Load_LocalPlayer", Playerlist[i], StartPoints[idx].transform.position);
            StartPoints.RemoveAt(idx);
        }

    }
    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint)
    {
        Debug.Log("생성!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);
    }
}
