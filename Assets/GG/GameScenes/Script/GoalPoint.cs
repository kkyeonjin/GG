using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GoalPoint : MonoBehaviour
{
    public PhotonView m_PV;

    public List<Transform> RespawnPos;
    public GameObject NextGoalPoint;

    public bool m_bNextScene = false;

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
                    if (m_bNextScene)
                    {
                        m_PV.RPC("Next_Phase", RpcTarget.All);
                    }
                    else
                    { 
                        m_PV.RPC("Increase_Count", RpcTarget.All);
                        GameMgr.Instance.Deliver_Record(true);
                        Allocate_Respawn();
                        //중간 지점 전달
                        this.gameObject.SetActive(false);//골인 두번 되지 않도록
                        NextGoalPoint.SetActive(true);//다음 골인 지점 활성화
                    }
                }
            }
        }
    }

    void Allocate_Respawn()
    {
        int idx = Random.Range(0, RespawnPos.Count);
        GameMgr.Instance.Set_ResumePoint(RespawnPos[idx].position);
        m_PV.RPC("Update_RespawnPos", RpcTarget.All, idx);
    }


    [PunRPC]
    void Increase_Count()
    {
        ++m_iTop3;
    }
    
    [PunRPC]
    void Update_RespawnPos(int RemovedIndex)
    {
        RespawnPos.RemoveAt(RemovedIndex);
    }

    [PunRPC]
    void Next_Phase()
    {
        SceneManager.LoadScene("Multi_Subway_Phase2");
    }
}
