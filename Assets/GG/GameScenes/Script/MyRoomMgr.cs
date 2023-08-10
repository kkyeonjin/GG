using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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



    private int m_CurrAvatar;
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

        int iSlotIndex = 0;
        for(int i=0;i<(int)Item.ITEM.END;++i)
        {
            if(InfoHandler.Instance.Get_Item_Num(i) >0)
            {
                Items[i].SetActive(true);
                Items[i].transform.position = ItemSlotPos[iSlotIndex++].position;
            }
        }
    }

    public void Select_Avatar(Player.CHARACTER eIndex)
    {
        m_Avatar[m_CurrAvatar].SetActive(false);
        m_Avatar[(int)eIndex].SetActive(true);

        m_CurrAvatar = (int)eIndex;
        //뒤에 현재 캐릭터 인덱스 정보 파일 수정
        InfoHandler.Instance.Set_CurrCharacter(m_CurrAvatar);
        InfoHandler.Instance.Save_Info();
    }
}
