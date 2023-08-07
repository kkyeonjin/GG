using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StoreUI : MonoBehaviour
{

    public Image m_SelectBuy;
    public bool m_bStartEnable = true;

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
    }

    public void Select_Yse()
    {
        //구매 하는 코드
        m_SelectBuy.gameObject.SetActive(false);
    }

    public void Select_No()
    {
        m_SelectBuy.gameObject.SetActive(false);
    }

    public void Select_Character()
    {
        //if(null == m_CurrCategory)
        //{
        //    m_CharacterCategory.SetActive(true);
        //    m_CurrCategory = m_CharacterCategory;
        //}
        //else
        //{
        //    m_CurrCategory.SetActive(false);
        //    m_CharacterCategory.SetActive(true);
        //    m_CurrCategory = m_CharacterCategory;
        //}
        m_CharacterCategory.SetActive(true);
        m_ItemCategory.SetActive(false);
    }
    public void Select_Item()
    {
        //if (null == m_CurrCategory)
        //{
        //    m_ItemCategory.SetActive(true);
        //    m_CurrCategory = m_ItemCategory;
        //}
        //else
        //{

        //   m_CurrCategory.SetActive(false);
        //   m_ItemCategory.SetActive(true);
        //   m_CurrCategory = m_ItemCategory;

        //}
        m_ItemCategory.SetActive(true);
        m_CharacterCategory.SetActive(false);
    }
}
