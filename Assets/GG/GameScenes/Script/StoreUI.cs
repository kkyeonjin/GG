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

    public GameObject NoMoneyUI;
    public GameObject YouAlreadyHaveUI;
    public GameObject BuyIt;
    public GameObject SelectBuy;

    //구매할 캐릭터 인덱스
    public int m_iCharacterIndex;
    public int m_iItemIndex;
    public int m_iPrice;

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
        m_SelectBuy.GetComponent<StoreUI>().Send_SelectObj(m_iCharacterIndex,m_iItemIndex,m_iPrice);
    }

    public void Send_SelectObj(int CharacterIndex, int ItemIndex, int iPrice)
    {
        m_iCharacterIndex = CharacterIndex;
        m_iItemIndex = ItemIndex;
        m_iPrice = iPrice;
    }

    public void Update_Money(int iMoney)
    {
        InfoHandler.Instance.Set_Money(-iMoney);
        string text = ""+InfoHandler.Instance.Get_Money();
        Money.text = text;
    }

    public void Select_Yse()
    {
       
        if (m_iCharacterIndex > -1)
            Buy_Character();
        else if (m_iItemIndex > -1)
            Buy_Item();

        //this.gameObject.SetActive(false);
        //Panel.SetActive(false);
    }

    public void Select_No()
    {
        SelectBuy.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        Panel.SetActive(false);
        m_iCharacterIndex = -1;
        m_iItemIndex = -1;
    }


    public void Buy_Character()//selectBuy에서 실행
    {
        Debug.Log("Character : " + m_iCharacterIndex);
        Debug.Log("Item : " + m_iItemIndex);
        //구매 하는 코드
        if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
        {
            this.gameObject.SetActive(false);

            int totalmoney = InfoHandler.Instance.Get_Money();
            if (totalmoney < m_iPrice)
            {
                NoMoneyUI.SetActive(true);
            }
            else
            {
                InfoHandler.Instance.Set_Character_Available(m_iCharacterIndex);
                Debug.Log("캐릭터" + m_iCharacterIndex + " 구매 완료!");
                InfoHandler.Instance.Save_Info();
                MoneyUI.Update_Money(m_iPrice);

                BuyIt.SetActive(true);
            }
        }
        else
        { 
            Debug.Log("이미 보유중입니다!");
            this.gameObject.SetActive(false);
            YouAlreadyHaveUI.SetActive(true);
        }
    }
    public void Buy_Item()
    {
        //구매 하는 코드
        int totalmoney = InfoHandler.Instance.Get_Money();
        this.gameObject.SetActive(false);
        if (totalmoney < m_iPrice)
        {
            NoMoneyUI.SetActive(true);
        }
        else
        {
            Debug.Log("아이템" + m_iItemIndex + " 구매 완료!");
            InfoHandler.Instance.Buy_Item(m_iItemIndex, 1);
            InfoHandler.Instance.Save_Info();
            Debug.Log(InfoHandler.Instance.Get_Item_Num(m_iItemIndex));
            MoneyUI.Update_Money(m_iPrice);
            BuyIt.SetActive(true);

        }

    }

}
