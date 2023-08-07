using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreUI : MonoBehaviour
{

    public Image m_SelectBuy;
    public bool m_bStartEnable = true;

    //������ ĳ���� �ε���
    public int m_iCharacterIndex;
    public int m_iItemIndex;

    //category
    public GameObject m_CharacterCategory;
    public GameObject m_ItemCategory;


    private GameObject m_CurrCategory;
    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(m_bStartEnable);

        if (m_CharacterCategory)
            m_CharacterCategory.SetActive(false);
        if (m_ItemCategory)
            m_ItemCategory.SetActive(false);

    }

    public void Click_Object()
    {
        
        m_SelectBuy.gameObject.SetActive(true);
        m_SelectBuy.GetComponent<StoreUI>().Send_SelectObj(m_iCharacterIndex,m_iItemIndex);
    }

    public void Send_SelectObj(int CharacterIndex, int ItemIndex)
    {
        m_iCharacterIndex = CharacterIndex;
        m_iItemIndex = ItemIndex;
    }

    public void Select_Yse()
    {
        Debug.Log("Character : " + m_iCharacterIndex);
        Debug.Log("Item : " + m_iItemIndex);
        //���� �ϴ� �ڵ�
        if (m_iCharacterIndex > -1)
            m_SelectBuy.GetComponent<StoreUI>().Buy_Character();
        else if (m_iItemIndex > -1)
            m_SelectBuy.GetComponent<StoreUI>().Buy_Item();

        m_SelectBuy.gameObject.SetActive(false);
    }

    public void Select_No()
    {
        m_SelectBuy.gameObject.SetActive(false);
        m_iCharacterIndex = -1;
        m_iItemIndex = -1;
    }

    public void Buy_Character()
    {
        if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
        {
            InfoHandler.Instance.Set_Character_Available(m_iCharacterIndex);
            Debug.Log("ĳ����" + m_iCharacterIndex + " ���� �Ϸ�!");
            InfoHandler.Instance.Save_Info();
        }
        else
            Debug.Log("�̹� �������Դϴ�!");
    }
    public void Buy_Item()
    {
        Debug.Log("������"+m_iItemIndex + " ���� �Ϸ�!");
    }


    public void Select_Character()
    {
        m_CharacterCategory.SetActive(true);
        m_ItemCategory.SetActive(false);
    }
    public void Select_Item()
    {
        m_ItemCategory.SetActive(true);
        m_CharacterCategory.SetActive(false);
    }
}