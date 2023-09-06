using System.Collections;
using System.Collections.Generic;
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
    //�÷��̾� �׾��� �� �ߴ� UI��

    public TextMeshProUGUI GoalTimer;
    public int fGoalTimerSec = 15;
    //1�� ���� �� Ÿ�̸�
    public TextMeshProUGUI GeneralTimer;
    public int fGeneralTimerMin = 3;
    public int fGeneralTimerSec = 0;
    //�Ϲ� Ÿ�̸�
    
    delegate void Calc_Timer();
    Calc_Timer Timer;

    private float m_fPassTime;
    private TextMeshProUGUI CurrTimer;

    //���� ���� �Ŀ� �ߴ� Ui��

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
            Debug.Log("�ҷ���!" + iInput.Get_ItemIndex());
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
    {//�������� �� ������ ��
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
