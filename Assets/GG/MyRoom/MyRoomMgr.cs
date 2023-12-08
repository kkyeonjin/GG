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
    public GameObject AvatarUI;
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

        CurrUI = AvatarUI;

        ManualString = new string[2, 8];

        ManualString[0, 0] = "탁자 아래와 같이 집 안에서 대피할 수 있는 안전한 대피 공간을 미리 파악해 둡니다.\n";
        ManualString[0, 1] = "가스를 미리 점검하고 차단합니다.\n";
        ManualString[0, 2] = "지진 발생 시 화재가 발생할 수 있으니 소화기를 준비해 두고, 사용방법을 알아 둡니다.\n";
        ManualString[0, 3] = "지진에 대비하여 비상용품과 가방을 준비해둡니다.\n";
        ManualString[0, 4] = "지진 시 엘리베이터를 타면 안됩니다.\n";

        ManualString[1, 0] = "열차 내에 있을 때 지진이 발생하면 기둥이나 손잡이를 꽉 잡아서 멎을 때까지 최대한 넘어지지 않도록 합니다.\n";
        ManualString[1, 1] = "지진의 진도가 5 이상이면 지하철의 운행이 일시적으로 정지됩니다.\n";
        ManualString[1, 2] = "지진으로 인해 정전이 발생할 수 있습니다. 정전을 대비하여 열차 칸마다 비상 손전등이 비치되어 있습니다.\n";
        ManualString[1, 3] = "정전으로 인해 열차 문의 개폐가 작동하지 않는다면 열차 문 옆에 있는 비상 레버, 콕크 등을 이용하여 수동 개방할 수 있습니다.\n";
        ManualString[1, 4] = "인파에 의한 사고를 방지하기 위해 주변 사람들과 거리를 유지하며 침착하게 이동합니다.\n";
        ManualString[1, 5] = "흔들림이 멎으면 지상으로 대피합니다.\n";

        //ManualString[1, 5] = "엘리베이터를 이용하지 않고 계단으로 걸어서 대피합니다.\n";
        //ManualString[1, 6] = "엘리베이터 탑승 중 지진 발생 시 재빨리 층 버튼들을 눌러서 아무 층에나 멈춰 세우도록 합니다.\n";
        //ManualString[1, 7] = "흔들림이 멎으면 지상으로 대피합니다.\n";

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

    public void Active_AvatarUI()
    {
        CurrUI.SetActive(false);
        AvatarUI.SetActive(true);
        CurrUI = AvatarUI;
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
        for(int i=0;i<(int)InfoHandler.HOUSE.END;++i)
        {
            Text += i + ". ";
            if (Manual[0, i] == true)
            {
                Text += ManualString[0, i];
            }
            else
                Text += "???.\n";

            Text += '\n';

        }
        HouseManual.text = Text;
        Text = "";

        //for (int i = 0; i < (int)InfoHandler.SUBWAY.END; ++i)
        for (int i = 0; i < 5; ++i)
        {
            Text += i + ". ";
            if (Manual[1, i] == true)
            {
                Text += ManualString[1, i];
            }
            else
                Text += "???.\n";

            Text += '\n';

        }
        SubwayManual.text = Text;

    }
}
