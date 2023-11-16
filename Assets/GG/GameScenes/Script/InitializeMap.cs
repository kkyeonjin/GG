using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InitializeMap : MonoBehaviour
{
    public List<GameObject> StartPoints;

    public PhotonView m_PV;

    //Phase 1 ����
    public List<GameObject> trainsInside;

    //Phase 2
    public bool Phase2 = false;
    public GameObject HierarchyObj;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�̴ϼȶ���¡ ��!");

        //GameMgr.Instance.Set_PlayerPos();
        //GameMgr.Instance.Set_Camera();
        if (PhotonNetwork.IsMasterClient == true)
        {
            int idx = Random.Range(0, StartPoints.Count);
            Load_LocalPlayer(StartPoints[idx].transform.position,idx);
            StartPoints.RemoveAt(idx);
       
            //NetworkManager.Instance.Initialize_Players_InMap(StartPoints);
            Initialize_Players_InMap();
        }
    }

    public void Initialize_Players_InMap()
    {//��� ���� ������ �� ��ü�� ��ġ ����

        Photon.Realtime.Player[] Playerlist = PhotonNetwork.PlayerListOthers;
        int iLength = Playerlist.Length;
        int i = 0;

        for ( i= 0; i < iLength; ++i)
        {
            int idx = Random.Range(0, StartPoints.Count);

            m_PV.RPC("Load_LocalPlayer", Playerlist[i], StartPoints[idx].transform.position,idx);
            StartPoints.RemoveAt(idx);
        }

        if (Phase2)
        {
            for (; i < 8; ++i)
            {
                int idx = Random.Range(0, StartPoints.Count);
                Load_AIPlayer(StartPoints[idx].transform.position);
                StartPoints.RemoveAt(idx);
            }
        }

    }

    //���� AI�� MasterClient�� �������ֱ�
    void Load_AIPlayer(Vector3 StartPoint)
    {
        Debug.Log("����!");
        NetworkManager.Instance.Instanctiate_AIPlayer(StartPoint, HierarchyObj.transform);
        GameMgr.Instance.Set_ResumePoint(StartPoint);
    }

    [PunRPC]
    void Load_LocalPlayer(Vector3 StartPoint,int idx)
    {
        Debug.Log("����!");
        NetworkManager.Instance.Instantiate_Player(StartPoint);
        GameMgr.Instance.Set_ResumePoint(StartPoint);
        if (Phase2 == false)
        {
            trainsInside[idx].SetActive(true);
        }
    }
    

}
