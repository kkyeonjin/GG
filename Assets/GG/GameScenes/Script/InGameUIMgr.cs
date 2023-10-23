using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIMgr : MonoBehaviour
{
    public static InGameUIMgr m_Instance;
    //�÷��̾� hp,stamina
    public StatusUI m_StatusUI;
    //�÷��̾� ������ ����
    public ItemSlotEffect[] m_StoreItemSlots;
    //�ΰ��� �߰� �������� ��ũ
    public RankSlot[] m_RankSlot;
    public Vector3[] m_RankSlotPos;
    private int m_iRankSlotIndex=0;

    //�÷��̾� �׾��� �� �ߴ� UI��
    public GameObject RespawnUI;
    public ItemSlotEffect ResumeSlot;
    public UIEffect RespawnTimeBar;
    public float m_fRespawnTime;
    private float m_fRespawnPassTime;

    //���� ��ŷ 
    public RankSlot[] m_ResultRankSlots;

    //Phase ���� �ð� Ÿ�̸�
    public TextMeshProUGUI GeneralTimer;
    public int iGeneralTimerMin = 2;
    public int iGeneralTimerSec = 0;

    //���� �÷��̾� ���� �� Ÿ�̸�
    public TextMeshProUGUI GoalTimer;
    public bool m_bGoalCountDown = false; //���� �÷��̾� ���� ����
    public int iGoalTimerSec = 15; //���� �÷��̾� ���� ���� ī��Ʈ�ٿ�
    public float m_fGoalTime = 0f; //���� ���� ��� �ð� ���� ����


    
    //�Ϲ� Ÿ�̸�
    
    delegate void Calc_Timer();
    Calc_Timer Timer;

    private float m_fPassTime;

    private bool m_bStopUpdating = false;

    void Awake()
    {

        var duplicated = FindObjectsOfType<InGameUIMgr>();

        if (duplicated.Length > 1)
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }


    }
    public static InGameUIMgr Instance
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
    // Start is called before the first frame update
    void Start()
    {
        Timer = Empty;
        Reset_Ranking();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    public void Set_PlayerStatus(Player LocalPlayer)
    {
        m_StatusUI.m_PlayerStatus =LocalPlayer.GetComponentInChildren<CharacterStatus>();

    }
    public void Set_Item(int iIndex, StoreItem iInput)
    {
        m_StoreItemSlots[iIndex].Set_Item(iInput);

        if (iInput.Get_ItemIndex() != (int)StoreItem.ITEM.END)
        {
            Debug.Log("�ҷ���!" + iInput.Get_ItemIndex());
            m_StoreItemSlots[iIndex].Set_IconImage(InfoHandler.Instance.Get_ItemIcon(iInput.Get_ItemIndex()));

            if (iInput.Get_ItemIndex() == (int)StoreItem.ITEM.RESUME)
            {
                ResumeSlot.Sharing_Material(m_StoreItemSlots[iIndex].Get_Material());
                ResumeSlot.Set_IconImage(InfoHandler.Instance.Get_ItemIcon(iInput.Get_ItemIndex()));
            }
        }
    }

    public string Get_Record()
    {
        return GeneralTimer.text;
    }

    public void Activate_RewpawnUI()
    {
        m_fRespawnPassTime = m_fRespawnTime;
        RespawnUI.SetActive(true);
        Timer += Respawn_CoolTime;
    }

    public void Player_Resume()
    {
        RespawnUI.SetActive(false);
    }

    void Respawn_CoolTime()
    {
        m_fRespawnPassTime -= Time.deltaTime;
        //Debug.Log(m_fRespawnPassTime);

        RespawnTimeBar.Get_PassedTime(m_fRespawnPassTime);

        if (Input.GetKeyDown(KeyCode.F))//������ ������ ��
        {
            Player_Resume();
            GameMgr.Instance.Use_ResumeItem();
            Timer -= Respawn_CoolTime;
        }

        if(m_fRespawnPassTime <=0f)
        {
            //��Ȱ
            Player_Resume();
            GameMgr.Instance.Resume_Onthepoints();
            Timer -= Respawn_CoolTime;
        }
    }


    void Empty()
    {

    }
     
    public void Start_Game()
    {
        GeneralTimer.gameObject.SetActive(true);

        m_fPassTime = 60 * iGeneralTimerMin + iGeneralTimerSec;

        Timer = Calculate_Time;
    }

    public void Start_GoalTimer()
    {//�������� �� ������ ��
        m_fGoalTime = iGoalTimerSec;
        Timer += Calculate_GoalTimer;
        if(m_bStopUpdating == false)
            GoalTimer.gameObject.SetActive(true);
    }

    public void Calculate_GoalTimer()
    {
        m_fGoalTime -= Time.deltaTime;
        if(m_fGoalTime <=0f)
        {
            m_bGoalCountDown = true;
            Timer -= Calculate_GoalTimer;
        }
        int Min = Mathf.Max(0, (int)m_fGoalTime / 60);
        int Sec = Mathf.Max(0, (int)m_fGoalTime % 60);

        string szMin = string.Format("{0:D2}", Min);
        string szSec = string.Format("{0:D2}", Sec);
        GoalTimer.text = szMin + ":" + szSec;
    }

    public void Player_GoalIn()
    {
        m_bStopUpdating = true;
        GoalTimer.gameObject.SetActive(false); 
    }

    void Calculate_Time()
    {
        m_fPassTime -= Time.deltaTime;
        int Min = Mathf.Max(0, (int)m_fPassTime / 60);
        int Sec = Mathf.Max(0, (int)m_fPassTime % 60);

        if (m_fPassTime <= 0f || m_bGoalCountDown)
        {
            GameMgr.Instance.Game_Over();
            GeneralTimer.text = "--:--";
            Timer = Empty;
        }
       
        if (m_bStopUpdating == false)
        {
            string szMin = string.Format("{0:D2}", Min);
            string szSec = string.Format("{0:D2}", Sec);
            GeneralTimer.text = szMin + ":" + szSec;
        }
    }

    public void Plug_Ranking(string playerName, string playerTime)
    {
        if (m_iRankSlotIndex >= 3)
            return;

        m_RankSlot[m_iRankSlotIndex].Set_Position(m_RankSlotPos[m_iRankSlotIndex]);
        m_RankSlot[m_iRankSlotIndex++].Get_SlotInfo(playerName, playerTime, m_iRankSlotIndex.ToString());
    }

    public void Reset_Ranking()//�߰� �� ����
    {
        m_RankSlot[0].ClearSlot();
        m_RankSlot[1].ClearSlot();
        m_RankSlot[2].ClearSlot();
    }

    public void ResultRanking(List<Tuple<string, Photon.Realtime.Player>> Ranking,List<Photon.Realtime.Player> GameOut)
    {
        int RankingSize = Ranking.Count;
        int iIndex = 0;
        for (int i = 0; i < RankingSize; ++i)
        {
            m_ResultRankSlots[iIndex++].Get_SlotInfo(Ranking[i].Item2.NickName, Ranking[i].Item1, (i+1).ToString());
        }
        RankingSize = GameOut.Count;
        for(int i=0;i< RankingSize; ++i)
        {
            m_ResultRankSlots[iIndex++].Get_SlotInfo(GameOut[i].NickName, "--:--", "over");
        }
    }
}
