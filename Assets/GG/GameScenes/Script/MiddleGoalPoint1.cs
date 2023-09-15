using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MiddleGoalPoint1 : MonoBehaviour
{
    public PhotonView m_PV;

    private int m_iTop3 = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (m_iTop3 < 3)
            {
                if (collision.gameObject.GetComponent<Player>().Is_MyPlayer())
                {
                    m_PV.RPC("Increase_Count", RpcTarget.All);
                    GameMgr.Instance.Deliver_Record(true);
                    //중간 지점 전달
                    this.gameObject.SetActive(false);//골인 두번 되지 않도록
                }
            }
        }
    }


    [PunRPC]
    void Increase_Count()
    {
        ++m_iTop3;
    }
}
