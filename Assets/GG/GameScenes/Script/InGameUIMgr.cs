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
    //플레이어 죽었을 때 뜨는 UI들

    public TextMeshProUGUI GoalTimer;
    public int fGoalTimerSec = 15;
    //1등 골인 후 타이머
    public TextMeshProUGUI GeneralTimer;
    public int fGeneralTimerMin = 3;
    public int fGeneralTimerSec = 0;
    //일반 타이머
    
    delegate void Calc_Timer();
    Calc_Timer Timer;

    private float m_fPassTime;
    private TextMeshProUGUI CurrTimer;

    //게임 끝난 후에 뜨는 Ui들

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
        }
    }

    void Empty()
    {

    }
     
    public void Start_Game()
    {
        GeneralTimer.gameObject.SetActive(true);

        m_fPassTime = 60 * fGeneralTimerMin + fGeneralTimerSec;
        CurrTimer = GeneralTimer;

        Timer = Calculate_Time;
    }

    public void Start_GoalTimer()
    {//누군가가 골에 들어왔을 때
        GeneralTimer.gameObject.SetActive(false);
        GoalTimer.gameObject.SetActive(true);
        m_fPassTime = fGoalTimerSec;

    }

    void Calculate_Time()
    {
        m_fPassTime -= Time.deltaTime;
        int Min = (int)m_fPassTime / 60;
        int Sec = (int)m_fPassTime % 60;

        string szMin = ""+Min, szSec = ""+Sec;
        szMin.PadLeft(2, '0');
        szSec.PadLeft(2, '0');

        CurrTimer.text = szMin + ":" + szSec;
    }

}
