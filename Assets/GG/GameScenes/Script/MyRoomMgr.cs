using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyRoomMgr : MonoBehaviour
{
    public GameObject m_Avatar1;
    public GameObject m_Avatar2;
    public GameObject m_Avatar3;
    public GameObject m_Avatar4;
    public GameObject m_Avatar5;
    public GameObject m_Avatar6;
    public GameObject m_Avatar7;
    public GameObject m_Avatar8;
    public GameObject m_Avatar9;

    private GameObject[] m_Avatar;

    public Transform[] ItemSlotPos;

    public GameObject[] Items;

    public TextMeshProUGUI m_PlayerInfo;
    public TextMeshProUGUI m_PlayerExp;
    public IconEffect m_PlayerExpImage;
    public TextMeshProUGUI m_PlayerMoney;

    public IconEffect[] m_StatusBar;

    public GameObject ManualUI;
    public GameObject ItemUI;
    private GameObject CurrUI;

    //Manual
    public GameObject HouseManuslObj;
    public GameObject SubwayManuslObj;
    public TextMeshProUGUI HouseManual;
    public TextMeshProUGUI SubwayManual;
    private string[,] ManualString;


    private int m_CurrAvatar;
    private MyRoomUI m_CurrCharacterUI;
    private AvatarStatus m_CurrStatus;
    // Start is called before the first frame update
    void Start()
    {
        m_Avatar = new GameObject[9];
        
        m_Avatar[0] = m_Avatar1;
        m_Avatar[1] = m_Avatar2;
        m_Avatar[2] = m_Avatar3;
        m_Avatar[3] = m_Avatar4;
        m_Avatar[4] = m_Avatar5;
        m_Avatar[5] = m_Avatar6;
        m_Avatar[6] = m_Avatar7;
        m_Avatar[7] = m_Avatar8;
        m_Avatar[8] = m_Avatar9;

        m_CurrAvatar = InfoHandler.Instance.Get_CurrCharacter();
        m_Avatar[m_CurrAvatar].SetActive(true);

        m_CurrStatus = m_Avatar[m_CurrAvatar].GetComponent<AvatarStatus>();

        int iSlotIndex = 0;
        for(int i=0;i<(int)StoreItem.ITEM.END;++i)
        {
            if(InfoHandler.Instance.Get_Item_Num(i) >0)
            {
                Items[i].SetActive(true);
                Items[i].transform.position = ItemSlotPos[iSlotIndex++].position;
            }
        }

        CurrUI = ItemUI;

        ManualString = new string[2, 8];

        ManualString[0, 0] = "TABLE";
        ManualString[0, 1] = "GAS";
        ManualString[0, 2] = "FIRE";
        ManualString[0, 3] = "PACKING";
        ManualString[0, 4] = "ELEVATOR";

        ManualString[1, 0] = "COLUMN";
        ManualString[1, 1] = "TRAINSTOP";
        ManualString[1, 2] = "BLACKOUT";
        ManualString[1, 3] = "LEVER";
        ManualString[1, 4] = "DISTANCE";
        ManualString[1, 5] = "ELEVATOR";
        ManualString[1, 6] = "ON_ELEVATOR";
        ManualString[1, 7] = "ESCAPE";
        
        Show_PlayerInfo();
        Show_CurrStatus();
    }

    public void Select_Avatar(MyRoomUI Input)
    {
        m_CurrCharacterUI.Avatar_Selected(false);
        m_Avatar[m_CurrAvatar].SetActive(false);
        m_Avatar[Input.Get_CharacterIndex()].SetActive(true);

        m_CurrCharacterUI = Input;
        m_CurrCharacterUI.Avatar_Selected(true);
        m_CurrAvatar = Input.Get_CharacterIndex();
        m_CurrStatus = m_Avatar[m_CurrAvatar].GetComponent<AvatarStatus>();
        Show_CurrStatus();
        //뒤에 현재 캐릭터 인덱스 정보 파일 수정
        InfoHandler.Instance.Set_CurrCharacter(m_CurrAvatar);
        InfoHandler.Instance.Save_Info();
    }

    public void Set_CurrAvatarUI(MyRoomUI Input)
    {
        m_CurrCharacterUI = Input;
    }
    public void Show_CurrStatus()
    {
        m_StatusBar[0].Set_LengthRatio(m_CurrStatus.Get_HP());
        m_StatusBar[1].Set_LengthRatio(m_CurrStatus.Get_Stamina());
        m_StatusBar[2].Set_LengthRatio(m_CurrStatus.Get_Speed());
        Debug.Log(m_CurrStatus.Get_HP() + " " + m_CurrStatus.Get_Stamina() + " " + m_CurrStatus.Get_Speed());
    }

    public void Show_PlayerInfo()
    {
        string text = "Lv. " + InfoHandler.Instance.Get_Level();
        m_PlayerInfo.text = text;

        float EXP = (float)InfoHandler.Instance.Get_Exp();
        float EXPMax = (float)InfoHandler.Instance.Get_ExpMax();
        int ExpRatio = (int)(EXP / EXPMax * 100);
        
        m_PlayerExp.text ="" + ExpRatio + "%";

        m_PlayerExpImage.Set_TotalLength(InfoHandler.Instance.Get_ExpMax());
        m_PlayerExpImage.Set_LengthRatio(InfoHandler.Instance.Get_Exp());

        m_PlayerMoney.text = "" + InfoHandler.Instance.Get_Money();
    }

    public void Active_ManualUI()
    {
        Initialize_Manual();
        CurrUI.SetActive(false);
        ManualUI.SetActive(true);
        CurrUI = ManualUI;
    }

    public void Active_ItemUI()
    {
        CurrUI.SetActive(false);
        ItemUI.SetActive(true);
        CurrUI = ItemUI;
    }

    public void HouseManual_On()
    {
        HouseManuslObj.SetActive(true);
        SubwayManuslObj.SetActive(false);
    }

    public void SubwayManual_On()
    {
        HouseManuslObj.SetActive(false);
        SubwayManuslObj.SetActive(true);
    }

    public void Initialize_Manual()
    {
        bool[,] Manual = InfoHandler.Instance.Get_UnlockedManual();

        string Text = "";
        //House
        for(int i=0;i<(int)PlayerInfo.HOUSE.END;++i)
        {
            Text += i + ". ";
            if (Manual[0, i] == true)
            {
                Text += ManualString[0, i];
            }
            else
                Text += "???.";

            Text += '\n';

        }
        HouseManual.text = Text;
        Text = "";
        for (int i = 0; i < (int)PlayerInfo.SUBWAY.END; ++i)
        {
            Text += i + ". ";
            if (Manual[1, i] == true)
            {
                Text += ManualString[1, i];
            }
            else
                Text += "???.";

            Text += '\n';

        }
        SubwayManual.text = Text;

    }
}
