using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIMgr : MonoBehaviour
{
    public static InGameUIMgr m_Instance;
    //플레이어 hp,stamina
    public StatusUI m_StatusUI;
    //플레이어 아이템 슬롯
    public ItemSlotEffect[] m_StoreItemSlots;

    //플레이어 죽엇을 때 뜨는 UI들
    public GameObject RespawnUI;
    public ItemSlotEffect ResumeSlot;
    public UIEffect RespawnTimeBar;
    public float m_fRespawnTime;
    private float m_fRespawnPassTime;



    public int iGoalTimerSec = 15;
    //1등 골인 후 타이머
    public TextMeshProUGUI GeneralTimer;
    public int iGeneralTimerMin = 3;
    public int iGeneralTimerSec = 0;
    //일반 타이머
    
    delegate void Calc_Timer();
    Calc_Timer Timer;

    private float m_fPassTime;

    private bool m_bStopUpdating = false;

    void Awake()
    {

        var duplicated = FindObjectsOfType<InGameUIMgr>();

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

        if(iInput.Get_ItemIndex() != (int)StoreItem.ITEM.END)
        {
            Debug.Log("불려짐!" + iInput.Get_ItemIndex());
            m_StoreItemSlots[iIndex].Set_IconImage(InfoHandler.Instance.Get_ItemIcon(iInput.Get_ItemIndex()));
            if(iInput.Get_ItemIndex() == (int)StoreItem.ITEM.RESUME)
                ResumeSlot = m_StoreItemSlots[iIndex];
        }
    }
    public void Activate_RewpawnUI()
    {
        m_fRespawnPassTime = m_fRespawnTime;
        RespawnUI.SetActive(true);
        Timer += Respawn_CoolTime;
    }

    public void Player_Resume()
    {
        Timer -= Respawn_CoolTime;
        RespawnUI.SetActive(false);
    }

    void Respawn_CoolTime()
    {
        m_fRespawnPassTime -= Time.deltaTime;
        RespawnTimeBar.Get_PassedTime(m_fRespawnPassTime);
        if (Input.GetKeyDown(KeyCode.F))//아이템 눌렀을 때
        {
            Player_Resume();
            GameMgr.Instance.Use_ResumeItem();
        }

        if(m_fRespawnPassTime <=0f)
        {
            //부활
            Player_Resume();
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
    {//누군가가 골에 들어왔을 때
        m_fPassTime = iGoalTimerSec;
    }


    public void Player_GoalIn()
    {
        m_bStopUpdating = true;
    }

    void Calculate_Time()
    {
        m_fPassTime -= Time.deltaTime;
        int Min = Mathf.Max(0, (int)m_fPassTime / 60);
        int Sec = Mathf.Max(0, (int)m_fPassTime % 60);

        if (m_fPassTime <= 0f)
        {
            GameMgr.Instance.Game_Over();
            Timer = Empty;
        }
       
        if (m_bStopUpdating == false)
        {
            string szMin = string.Format("{0:D2}", Min);
            string szSec = string.Format("{0:D2}", Sec);
            GeneralTimer.text = szMin + ":" + szSec;
        }
    }

}
