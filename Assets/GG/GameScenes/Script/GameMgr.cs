using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;


public class GameMgr : MonoBehaviour
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
            m_ItemSlot = new StoreItem[2];    
            
            for (int i=0;i<2;++i)
            {
                int itemIndex = HoldingItem[i, 0];

                switch (itemIndex)
                {
                    case 0:
                        {
                            m_ItemSlot[i] = Get_Instance<StoreItem_Resume>();
                            Debug.Log(m_ItemSlot[i]);
                            break;
                        }
                    case 1:
                        {
                            m_ItemSlot[i] = Get_Instance<StoreItem_Death>();
                            Debug.Log(m_ItemSlot[i]);
                            break;
                        }
                    case 2:
                        {
                            m_ItemSlot[i] = Get_Instance<StoreItem_Adrenaline>();
                            Debug.Log(m_ItemSlot[i]);
                            break;
                        }
                    case 3:
                        {
                            m_ItemSlot[i] = Get_Instance<StoreItem_Potion>();
                            Debug.Log(m_ItemSlot[i]);

                            break;
                        }
                    case 4:
                        {
                            m_ItemSlot[i] = Get_Instance<StoreItem_Invincible>();
                            Debug.Log(m_ItemSlot[i]);
                            break;
                        }

                }

                if (m_ItemSlot[i] == null)
                    m_ItemSlot[i] = Get_Instance<StoreItem>();

                m_ItemSlot[i].Set_Num(HoldingItem[i, 1]);

                InGameUIMgr.Instance.Set_Item(i, m_ItemSlot[i]);
               
            }
            Debug.Log(m_ItemSlot[0] + " " + m_ItemSlot[1]);
        }
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
//////////////////////멀티 버전에서 사용하는 함수//////////////////////////////////////////////////
    public void Load_LocalPlayer(Vector3 vStartPoint)
    {
        Debug.LogWarning("호출");
        NetworkManager.Instance.Instantiate_Player(vStartPoint);
    }
    public void Set_LocalPlayer(Player LocalPlayer)
    {
        m_LocalPlayer = LocalPlayer;
        m_LocalPlayerObj = LocalPlayer.gameObject;
        if (m_bInGame)
            InGameUIMgr.Instance.Set_PlayerStatus(m_LocalPlayer);
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
    public void BroadCast_Death()//아이템 쓴 당사자가 부를 함수
    {
        m_PV.RPC("MakeLocalPlayer_Die", RpcTarget.Others);
    }
    
    [PunRPC]
    void MakeLocalPlayer_Die()//죽을 플레이어들이 받을 함수
    {
        m_LocalPlayer.Immediate_Death();    
    }

 ///////////////////////////////////////////////////////////////////////////////////////////////

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
    
    public T Get_Instance<T>()
    {
        GameObject gameobject = new GameObject();
        gameobject.AddComponent(typeof(T));

        T Instance = gameobject.GetComponent<T>();

        return Instance;
    }
}
