using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using Photon.Pun;
public class GameMgr : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameMgr m_Instance = null;

    public Player m_LocalPlayer;
    public GameObject m_LocalPlayerObj;
    public bool m_bInGame = false;
    
    //인게임에서 상점 아이템 관련해서 많이 쓰임
    public PhotonView m_PV;
    private StoreItem[] m_ItemSlot;

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
            }
        }


    }
    void Start()
    {
       if(m_bInGame)
        {
            //인게임일 때 아이템들 아이콘, 인스턴스 등 생성
            int[,] HoldingItem = InfoHandler.Instance.Get_HoldingItem();
            
            for(int i=0;i<2;++i)
            {
                m_ItemSlot[i] = Create_ItemInstance(HoldingItem[i, 0]);
                m_ItemSlot[i].Set_Num(HoldingItem[i, 1]);
                InGameUIMgr.Instance.Set_Item(i, m_ItemSlot[i]);
                Debug.Log("불렸다!");
            }
        }
    }
    private StoreItem Create_ItemInstance(int itemIndex)
    {
        StoreItem Temp;
        
        switch(itemIndex)
        {
            case 0:
                Temp = new StoreItem_Resume();
                return Temp;
            case 1:
                Temp = new StoreItem_Death();
                return Temp;
            case 2:
                Temp = new StoreItem_Adrenaline();
                return Temp;
            case 3:
                Temp = new StoreItem_Potion();
                return Temp;
            case 4:
                Temp = new StoreItem_Invincible();
                return Temp;
        }
        return new StoreItem();
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

    //////////인게임에서 쓰일 함수들//././///////
    void Update()
    {//임시로 키 만들어놓음
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_ItemSlot[0].Is_Usable())
            {
                m_ItemSlot[0].Use_Item();
                //InGameUIMgr.Instance.Use_Item();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_ItemSlot[1].Is_Usable())
            {
                m_ItemSlot[1].Use_Item();
            }
        }
        //else if(Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    if (m_ItemSlot[2].Is_Usable())
        //        m_ItemSlot[2].Use_Item();
        //}
    }

    public void BroadCast_Death()//아이템 쓴 당사자가 부를 함수
    {
        m_PV.RPC("MakeLocalPlayer_Die", RpcTarget.Others);
    }
    
    [PunRPC]
    void MakeLocalPlayer_Die()//죽을 플레이어들이 받을 함수
    {
        m_LocalPlayer.Immediate_Death();    
    }
}
