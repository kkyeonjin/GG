using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameMgr m_Instance = null;

    public Player m_LocalPlayer;
    public GameObject m_LocalPlayerObj;

    private bool[] IsOccupied;

    void Awake()
    {
        
        var duplicated = FindObjectsOfType<GameMgr>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == m_Instance)
            {
                m_Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }


    }
    void Start()
    {
       
    }

    public static GameMgr Instance
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

    public void Load_LocalPlayer(Vector3 vStartPoint)
    {
        Debug.LogWarning("호출");
        NetworkManager.Instance.Instantiate_Player(vStartPoint);
    }
    public void Set_PlayerPos(Vector3 PlayerPoint)
    {//임시
        if (null != m_LocalPlayerObj)
            m_LocalPlayerObj.transform.position = PlayerPoint;
    }
    public void Destroy_Myself()
    {
        Destroy(this.gameObject);
    }
    public void Set_Camera()
    {

        CinemachineVirtualCamera Temp = FindObjectOfType<CinemachineVirtualCamera>();
        if(null!=Temp && null != m_LocalPlayer)
            m_LocalPlayer.Set_Camera(Temp);        
    }

    public void Change_Avatar(int iIndex)
    {
        m_LocalPlayer.GetComponentInChildren<ChangeAvatar>().Change_Avatar(iIndex);
    }

    public void OnPhotonSerializeView(PhotonStream photonstream, PhotonMessageInfo photonmessageinfo)
    {

    }

    public void Reward_Player()//게임 끝난후 로비로 돌아감(멀티)
    {
        //사용한 아이템 개수 Info에 업데이트
        //보상 등 여기서 주면 될듯

    }

}
