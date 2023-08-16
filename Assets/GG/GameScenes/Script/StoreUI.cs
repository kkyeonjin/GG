using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StoreUI : MonoBehaviour
{

    public TextMeshProUGUI Money;
    public StoreUI MoneyUI;

    public GameObject Panel; 
    public Image m_SelectBuy;
    public bool m_bStartEnable = true;

    //구매할 캐릭터 인덱스
    public int m_iCharacterIndex;
    public int m_iItemIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.SetActive(m_bStartEnable);
        if (Money != null)
            Update_Money(0);
    }

    public void Click_Object()
    {
        Panel.SetActive(true);
        m_SelectBuy.gameObject.SetActive(true);
        m_SelectBuy.GetComponent<StoreUI>().Send_SelectObj(m_iCharacterIndex,m_iItemIndex);
    }

    public void Send_SelectObj(int CharacterIndex, int ItemIndex)
    {
        m_iCharacterIndex = CharacterIndex;
        m_iItemIndex = ItemIndex;
    }

    public void Update_Money(int iMoney)
    {
        InfoHandler.Instance.Set_Money(-iMoney);
        string text = "Money: " + InfoHandler.Instance.Get_Money();
        Money.text = text;
    }

    public void Select_Yse()
    {
       
        if (m_iCharacterIndex > -1)
            Buy_Character();
        else if (m_iItemIndex > -1)
            Buy_Item();

        //this.gameObject.SetActive(false);
        Panel.SetActive(false);
    }

    public void Select_No()
    {
       // m_SelectBuy.gameObject.SetActive(false);
        Panel.SetActive(false);
        m_iCharacterIndex = -1;
        m_iItemIndex = -1;
    }

    public void Buy_Character()
    {
        Debug.Log("Character : " + m_iCharacterIndex);
        Debug.Log("Item : " + m_iItemIndex);
        //구매 하는 코드
        if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
        {
            InfoHandler.Instance.Set_Character_Available(m_iCharacterIndex);
            Debug.Log("캐릭터" + m_iCharacterIndex + " 구매 완료!");
            InfoHandler.Instance.Save_Info();
            MoneyUI.Update_Money(10);
        }
        else
            Debug.Log("이미 보유중입니다!");
    }
    public void Buy_Item()
    {
        Debug.Log("Character : " + m_iCharacterIndex);
        Debug.Log("Item : " + m_iItemIndex);
        //구매 하는 코드
        Debug.Log("아이템"+m_iItemIndex + " 구매 완료!");
        InfoHandler.Instance.Buy_Item(m_iItemIndex, 1);
        InfoHandler.Instance.Save_Info();
        Debug.Log(InfoHandler.Instance.Get_Item_Num(m_iItemIndex));
        MoneyUI.Update_Money(10);

    }

}
