using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public static GameMgr m_Instance = null;

    public float fResultScreenTime = 10f;
    public float fCeremonyTime = 5f;

    public GameObject ResultScreen;
    public GameObject GameScreen;

    public Player m_LocalPlayer;
    public GameObject m_LocalPlayerObj;
    public bool m_bInGame = false;
    public bool m_bMulti = false;

    public float fDelayStartTime = 2f;

    private bool m_bSomeOneFirst = false;
    private bool m_bIamTheFirst = false;

    private Vector3 vResumePoint;

    //인게임에서 상점 아이템 관련해서 많이 쓰임
    public PhotonView m_PV;

    private StoreItem[] m_ItemSlot;
    private StoreItem_Resume ItemResume;

    //점수 관련

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
        if (m_bInGame && m_bMulti)
        {
            //인게임일 때 아이템들 아이콘, 인스턴스 등 생성
            int[,] HoldingItem = InfoHandler.Instance.Get_HoldingItem();
            m_ItemSlot = new StoreItem[2];

            for (int i = 0; i < 2; ++i)
            {
                int itemIndex = HoldingItem[i, 0];

                switch (itemIndex)
                {
                    case 0:
                        {
                            ItemResume = Get_Instance<StoreItem_Resume>();
                            m_ItemSlot[i] = ItemResume;
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

            Invoke("BroadCast_StartGame", fDelayStartTime);
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
        if (null != Temp && null != m_LocalPlayer)
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
    public void BroadCast_StartGame()
    {
        m_PV.RPC("Start_Game", RpcTarget.All);
    }
    public void BroadCast_TimeAttack()
    {
        m_PV.RPC("SomeOne_GoalIn", RpcTarget.All);
    }

    [PunRPC]
    void MakeLocalPlayer_Die()//죽을 플레이어들이 받을 함수
    {
        m_LocalPlayer.Immediate_Death();
    }
    [PunRPC]
    void Start_Game()
    {
        InGameUIMgr.Instance.Start_Game();
    }
    [PunRPC]
    void SomeOne_GoalIn()
    {
        m_bSomeOneFirst = true;
        InGameUIMgr.Instance.Start_GoalTimer();
    }
    public void Game_Over()
    {
        Invoke("Show_ResultScreen", fCeremonyTime);
    }

    public void Player_GoalIn()//싱글 모드
    {
        Debug.Log("Player GoalIn!");

        Invoke("Show_ResultScreen", fCeremonyTime);

    }

    public void Use_ResumeItem()//즉부 아이템 썻을 때
    {
        ItemResume.Consume_Item();
        m_LocalPlayer.Immediate_Resume();
    }

    public void Resume_Onthepoints()//거점 부활
    {
        m_LocalPlayer.Resume(vResumePoint);
    }

    public void Set_ResumePoint(Vector3 vPoint)//중간 골인 지점 지나면 거점 부활 지점 변경해줌
    {
        vResumePoint = vPoint;
        Debug.Log(vResumePoint);
    }

    public void Player_GoalIn(bool IsMine)//멀티 모드
    {
        if (IsMine)
        {
            InGameUIMgr.Instance.Player_GoalIn();
            if (m_bSomeOneFirst == false)
            {
                BroadCast_TimeAttack();
                m_bIamTheFirst = true;
            }
        }
    }

    public void Player_NextPhase()
    {
        Debug.Log("Go to NextPhase!");
    }

    public void Deliver_Record(bool IsMiddleGoal)
    {//이걸 부른 사람은 골인 하고 top3안에 들어간 사람
        string Name = NetworkManager.Instance.Get_Playername();
        string Record = InGameUIMgr.Instance.Get_Record();

        m_PV.RPC("Receive_Record", RpcTarget.All, IsMiddleGoal, Name, Record);
    }

    [PunRPC]
    void Receive_Record(bool IsMiddleGoal, string Name, string Record)
    {
        if(IsMiddleGoal)
        {
            InGameUIMgr.Instance.Plug_Ranking(Name, Record);
        }
    }

    //////////인게임에서 쓰일 함수들//././///////
    void Update()
    {
        if (false == m_bInGame && m_bMulti)
            return;

        //임시로 키 만들어놓음
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_ItemSlot[0].Is_Usable() && (false == m_ItemSlot[0].Get_Activated()))
            {
                m_ItemSlot[0].Use_Item();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)&&(false == m_ItemSlot[1].Get_Activated()))
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
///////////////////////////////////////////////////////////////////////////////////////////////
    public void Reward_Player()//게임 끝난후 로비로 돌아감
    {
        //사용한 아이템 개수 Info에 업데이트
        //보상 등 여기서 주면 될듯

    }
    
    void Show_ResultScreen()
    {
        GameScreen.SetActive(false);
        ResultScreen.SetActive(true);
        Invoke("BackToLobby", fResultScreenTime);
    }
    
    void BackToLobby()
    {
        if (m_bMulti)
        {
             if(m_bIamTheFirst)//1등이 부르기
               m_PV.RPC("BacktoMultiLobby", RpcTarget.All);
        }
        else
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    [PunRPC]
    void BacktoMultiLobby()
    {
        Debug.Log("멀티 로비로 가기");
        NetworkManager.Instance.StartGame("MultiLobby");
    }
}
