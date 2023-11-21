using System.Collections;
using System.Collections.Generic;
using System;
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
    public bool m_bIsPhase1 = false;

    public float fDelayStartTime = 2f;

    public CinemachineVirtualCamera FollowCamera;

    private bool m_bSomeOneFirst = false;
    private bool m_bIamTheFirst = false;
    private bool m_bLocalPlayerGoalIn = false;

    public RewardUI rewardui;

    private Vector3 vResumePoint;

    //인게임에서 상점 아이템 관련해서 많이 쓰임
    public PhotonView m_PV;

    private StoreItem[] m_ItemSlot;
    private StoreItem_Resume ItemResume;

    //점수, 결과 관련
    private List<Tuple<string, Photon.Realtime.Player>> Ranking;
    private List<Photon.Realtime.Player> GameOut;

    private bool m_bDeathCount = false;
    private float m_fDeathTimer = 0f;
    public GameObject DeathUI;
    int iRank = -1;

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

            Ranking = new List<Tuple<string, Photon.Realtime.Player>>();
            GameOut = new List<Photon.Realtime.Player>();
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

    public void Player_GoalIn()//싱글 모드
    {
        Debug.Log("player GoalIn!");

        Invoke("Show_ResultScreen", fCeremonyTime);

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

        if (m_bIsPhase1)
            m_LocalPlayer.Off_Jump();

        if (m_bInGame)
            InGameUIMgr.Instance.Set_PlayerStatus(m_LocalPlayer);
    }
    public void Set_Camera()
    {
        m_LocalPlayer.Set_Camera(FollowCamera);
        FollowCamera.Follow = m_LocalPlayer.transform;
    }

    public void Change_Avatar(int iIndex)
    {
        m_LocalPlayer.GetComponentInChildren<ChangeAvatar>().Change_Avatar(iIndex);
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
    [PunRPC]
    void GameOut_Player(Photon.Realtime.Player player)
    {
        GameOut.Add(player);
    }

    public void Game_Over()
    {
        if (m_bLocalPlayerGoalIn == false)
        {
            m_PV.RPC("GameOut_Player", RpcTarget.All, PhotonNetwork.LocalPlayer);
            InGameUIMgr.Instance.Active_GameOver();
        }

        Invoke("Show_ResultScreen", fCeremonyTime);
    }
    public void Trigger_Death()
    {
        m_fDeathTimer = 5f;
        m_bDeathCount = true;
        DeathUI.SetActive(true);
    }
    
    void Kill_Player()
    {
        if(m_LocalPlayer.Apply_DeathItem() == false)//막았음
        {
            //대충 막는 이펙트
        }
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
            iRank = Ranking.Count + 1;
            m_PV.RPC("Add_RankingList", RpcTarget.All, PhotonNetwork.LocalPlayer, InGameUIMgr.Instance.Get_Record());
            
            m_bLocalPlayerGoalIn = true;

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

    private void Calculate_Ranking()
    {//일단 들어온 시간 순서대로
        Ranking.Sort();
    }

    [PunRPC]
    void Receive_Record(bool IsMiddleGoal, string Name, string Record)
    {
        if(IsMiddleGoal)
        {
            InGameUIMgr.Instance.Plug_Ranking(Name, Record);
        }
    }
    [PunRPC]
    void Add_RankingList(Photon.Realtime.Player player, string record)
    {
        Ranking.Add(new Tuple< string, Photon.Realtime.Player>(record, player));
    }

    //////////인게임에서 쓰일 함수들//././///////
    void Update()
    {
        if (false == m_bInGame )
            return;

        //임시로 키 만들어놓음
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_ItemSlot[0].Is_Usable())
            {
                m_ItemSlot[0].Use_Item();
                InfoHandler.Instance.Use_HoldingItem(m_ItemSlot[0].Get_ItemIndex());
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_ItemSlot[1].Is_Usable())
            {
                m_ItemSlot[1].Use_Item();
                InfoHandler.Instance.Use_HoldingItem(m_ItemSlot[1].Get_ItemIndex());
            }
        }

        if(m_bDeathCount)
        {
            m_fDeathTimer -= Time.deltaTime;
            if(m_fDeathTimer<=0f)
            {
                m_bDeathCount = false;
                DeathUI.SetActive(false);
                //죽여라
                Kill_Player();
            }
        }
     
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
        if(m_bLocalPlayerGoalIn)
        {
            int Exp = 5;
            int Gold = 10;
            switch(iRank)
            {
                case 1:
                    Exp = 100;
                    Gold = 50;
                    InfoHandler.Instance.Set_Money(50);
                    break;
                case 2:
                    Exp = 80;
                    Gold = 45;

                    InfoHandler.Instance.Set_Money(45);
                    break;
                case 3:
                    Exp = 70;
                    Gold = 40;

                    InfoHandler.Instance.Set_Money(40);
                    break;
                case 4:
                    Exp = 60;
                    Gold = 35;

                    InfoHandler.Instance.Set_Money(35);
                    break;
                case 5:
                    Exp = 50;
                    Gold = 30;

                    InfoHandler.Instance.Set_Money(30);
                    break;
                case 6:
                    Exp = 40;
                    Gold = 25;

                    InfoHandler.Instance.Set_Money(25);
                    break;
                case 7:
                    Exp = 30;
                    Gold = 20;

                    InfoHandler.Instance.Set_Money(20);
                    break;
                case 8:
                    Exp = 10;
                    Gold = 15;

                    InfoHandler.Instance.Set_Money(15);
                    break;
                default:
                    Exp = 5;
                    Gold = 10;

                    InfoHandler.Instance.Set_Money(10);
                    break;
            }

            Exp = (int)((float)Exp * 0.01f * SubwayInventory.instance.Get_OrderGauge());
            InfoHandler.Instance.Set_Exp(Exp);

            rewardui.Get_Reward(Gold, Exp);
        }

    }
    
    void Show_ResultScreen()
    {
        GameScreen.SetActive(false);
        ResultScreen.SetActive(true);
        Reward_Player();

        InGameUIMgr.Instance.ResultRanking(Ranking, GameOut);
        Invoke("BackToLobby", fResultScreenTime);
    }
    
    void BackToLobby()
    {
        if (m_bMulti)
        {
             if(m_bIamTheFirst)//1등이 부르기
               m_PV.RPC("BacktoMultiLobby", RpcTarget.All);
        }
  
    }

    [PunRPC]
    void BacktoMultiLobby()
    {
        Debug.Log("멀티 로비로 가기");
        NetworkManager.Instance.StartGame("MultiLobby");
    }
}
