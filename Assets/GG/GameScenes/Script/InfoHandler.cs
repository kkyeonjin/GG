using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.UI;

public class InfoHandler : MonoBehaviour
{
    private PlayerInfo m_Playerinfo;
    private static InfoHandler m_Instance = null;

    public Image[] m_ItemIcons;

    //로비에서 아이템 선택창
    public ItemSelectUI[] m_HoldingItemUI;
    public ItemSelectUI[] m_SelectItemUI;

    private int[] m_SlotIndex;
    private int[,] m_HoldingItem;

    void Awake()
    {
        var duplicated = FindObjectsOfType<InfoHandler>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else if (null == m_Instance)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        m_HoldingItem = new int[2, 2];
        m_HoldingItem[0, 0] = (int)StoreItem.ITEM.END;
        m_HoldingItem[0, 1] = -1;
        m_HoldingItem[1, 0] = (int)StoreItem.ITEM.END;
        m_HoldingItem[1, 1] = -1;

        m_SlotIndex = new int[2];
        m_SlotIndex[0] = -1;
        m_SlotIndex[1] = -1;

    }
    public static InfoHandler Instance
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
        //저장
        //m_Playerinfo = new PlayerInfo();
        //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "PlayerInfo"), FileMode.Create);
        //fileStream.Close();
        //string jsondata = JsonConvert.SerializeObject(m_Playerinfo);
        //Debug.Log(jsondata);
        //byte[] data = Encoding.UTF8.GetBytes(jsondata);

        //File.WriteAllText(Application.streamingAssetsPath + "/PlayerInfo.json", jsondata);

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "PlayerInfo"), FileMode.Open, FileAccess.Read);
    
         byte[] data = new byte[fileStream.Length];
         fileStream.Read(data, 0, data.Length);

         string jsondata = Encoding.UTF8.GetString(data);
         Debug.Log(jsondata);
         fileStream.Close();

         m_Playerinfo = JsonConvert.DeserializeObject<PlayerInfo>(jsondata);

         Debug.Log(m_Playerinfo.Get_Level());
     
    }
    public static void Initizlize_Player(string name)//처음 게임 시작할 때
    {
        PlayerInfo Info = new PlayerInfo(true);
        Info.Set_Name(name);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "PlayerInfo"), FileMode.Create);
        fileStream.Close();
        string jsondata = JsonConvert.SerializeObject(Info);
        Debug.Log(jsondata);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);

        File.WriteAllText(Application.streamingAssetsPath + "/PlayerInfo.json", jsondata);
    }
    public void Reload_HoldingSlots()//게임 끝나고 미리 장착했던 아이템 다시 불러옴
    {
        for(int i=0;i<2;++i)
        {
            if (m_SlotIndex[i] > -1)
            {
                if (Get_Item_Num(m_HoldingItem[i, 0]) != (int)StoreItem.ITEM.END)
                {
                    m_HoldingItemUI[i].Set_Image(Get_ItemIcon(m_HoldingItem[i, 0]));
                    m_HoldingItemUI[i].Have_Items(true);
                    //m_SelectItemUI[m_SlotIndex[i]].Item_Selected();
                }
                else
                {//다 써서 비어있음
                    m_HoldingItem[i, 0] = (int)StoreItem.ITEM.END;
                    m_HoldingItem[i, 1] = -1;
                    m_SlotIndex[i] = -1;
                }
            }
        }
    }
    public void Set_HoldingItemSlots(ItemSelectUI[] Input)
    {
        m_HoldingItemUI = Input;

    }
    public void Set_SelectItemSlots(ItemSelectUI[] Input)
    {
        m_SelectItemUI = Input;
    }
    public int[] HoldingSlotIndex()
    {
        return m_SlotIndex;
    }
    public bool Set_HoldingItem(ItemSelectUI iInput)
    {
        if (iInput.Is_Selected())
            return false;

        for (int i = 0; i < 2; ++i)
        {
            if (m_HoldingItem[i, 0] == (int)StoreItem.ITEM.END)
            {
                m_HoldingItem[i, 0] = iInput.Get_Index();//아이템 타입 enum저장
                m_HoldingItemUI[i].Set_Image(iInput.Get_Image());
                m_HoldingItemUI[i].Have_Items(true);
                m_SlotIndex[i] = iInput.Get_SlotIndex();
                //개수 설정
                return true;
            }
        }
        return false;//빈자리 없음
    }
    public void Set_Unholding(int iIndex)
    {
        m_HoldingItem[iIndex, 0] = (int)StoreItem.ITEM.END;
        m_HoldingItem[iIndex, 1] = -1;

        m_SelectItemUI[m_SlotIndex[iIndex]].Slot_Selected(false);
        m_SlotIndex[iIndex] = -1;

    }

    public void Clear_HoldingItem()
    {
        m_HoldingItem[0, 0] = (int)StoreItem.ITEM.END;
        m_HoldingItem[0, 1] = -1;
        m_HoldingItem[1, 0] = (int)StoreItem.ITEM.END;
        m_HoldingItem[1, 1] = -1;

        m_SlotIndex[0] = -1;
        m_SlotIndex[1] = -1;
    }

    public int[,] Get_HoldingItem()
    {
        return m_HoldingItem;
    }
    public Image Get_ItemIcon(int iIndex)
    {
        return m_ItemIcons[iIndex];
    }
    public int Get_Level()
    {
        return m_Playerinfo.Get_Level();
    }

    public int Get_Exp()
    {
        return m_Playerinfo.Get_Exp();
    }

    public int Get_ExpMax()
    {
        return m_Playerinfo.Get_ExpMax();
    }

    public int Get_Money()
    {
        return m_Playerinfo.Get_Money();
    }
    public int Get_CurrCharacter()
    {
        return m_Playerinfo.Get_CurrCharacter();
    }
    public string Get_Name()
    {
        return m_Playerinfo.Get_Name();
    }
    public void Set_NickName(string name)
    {
        m_Playerinfo.Set_Name(name);
    }
    public void Set_Exp(int iExp)
    {
        m_Playerinfo.Set_Exp(iExp);
    }
    public void Set_Money(int iMoney)
    {
        m_Playerinfo.Set_Money(iMoney);
    }


    public void Set_CurrCharacter(int iCurrIndex)
    {
        m_Playerinfo.Set_CurrCharacter(iCurrIndex);
        Save_Info();
    }

    public bool Is_Character_Available(int iIndex)
    {
        return m_Playerinfo.Is_Character_Available(iIndex);
    }

    public void Set_Character_Available(int iIndex)
    {
        m_Playerinfo.Set_Character_Available(iIndex);
    }

    public void Buy_Item(int iIndex, int iNum)
    {
        m_Playerinfo.Buy_Item(iIndex, iNum);
    }

    public int Get_Item_Num(int iIndex)
    {
        return m_Playerinfo.Get_Item_Num(iIndex);
    }

    public void Save_Info()
    {
        //json 파일 부분만 수정하는 방법이 뭐냐 대체
        //json생성(FileMode.Create) & 저장(FileAccess.Write)
        //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.dataPath, "PlayerInfo"), FileMode.Create, FileAccess.Write);
        string jsondata = JsonConvert.SerializeObject(m_Playerinfo);
        Debug.Log(jsondata);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);
    
        File.WriteAllText(Application.streamingAssetsPath + "/PlayerInfo.json",jsondata);

        //fileStream.Write(data, 0, data.Length);
        //fileStream.Close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
