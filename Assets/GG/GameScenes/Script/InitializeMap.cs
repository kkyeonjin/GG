using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InitializeMap : MonoBehaviour
{
    public List<GameObject> StartPoints;
    public bool bInGame = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("¿Ã¥œº»∂Û¿Ã¬° ∏ !");

        //GameMgr.Instance.Set_PlayerPos();
        //GameMgr.Instance.Set_Camera();
        if (bInGame && PhotonNetwork.IsMasterClient == true)
        {
            int idx = Random.Range(0, StartPoints.Count);
            GameMgr.Instance.Load_LocalPlayer(StartPoints[idx].transform.position);
            NetworkManager.Instance.Initialize_Players_InMap(StartPoints);

        }
        else//∑Œ∫Òø°º≠ º≥¡§
        {
            //int idx = Random.Range(0, StartPoints.Count);
            //GameMgr.Instance.Load_LocalPlayer(StartPoints[idx].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
