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

    public IconEffect[] m_StatusBar;


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
        for(int i=0;i<(int)Item.ITEM.END;++i)
        {
            if(InfoHandler.Instance.Get_Item_Num(i) >0)
            {
                Items[i].SetActive(true);
                Items[i].transform.position = ItemSlotPos[iSlotIndex++].position;
            }
        }

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
        //�ڿ� ���� ĳ���� �ε��� ���� ���� ����
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
        string text = "Level : " + InfoHandler.Instance.Get_Level();
        m_PlayerInfo.text = text;
    }

}
