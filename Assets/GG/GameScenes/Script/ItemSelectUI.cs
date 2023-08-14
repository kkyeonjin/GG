using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{
    public Image m_MyImage;
    public Button m_MyButton;

    Sprite m_ItemEmage;
    Sprite m_EmptyImage;

    Color m_Emptycolor;
    Color m_Itemcolor;

    public int iHoldingSlotIndex;

    Item.ITEM m_eIndex = Item.ITEM.END;

    public bool isSlotsParent = false;
    public bool isHoldingSlotParent = false;
    public ItemSelectUI[] m_Slots;

    //select slot ¿ë
    bool m_bSelected = false;
    public int m_iSelectSlotIndex = -1;

    private void Awake()
    {
        m_EmptyImage = m_MyImage.sprite;
        m_Emptycolor = m_MyImage.color;

        m_Itemcolor = new Color(1f, 1f, 1f, 1f);
    }
    void Start()
    {
        if (isSlotsParent == true)
        {
            int iSlotIndex = 0;
            for (int i = 0; i < 5; ++i)
            {
                if (InfoHandler.Instance.Get_Item_Num(i) > 0)
                {
                    m_Slots[iSlotIndex].Set_Image(InfoHandler.Instance.Get_ItemIcon(i));
                    m_Slots[iSlotIndex].Have_Items(true);
                    m_Slots[iSlotIndex++].Set_Index(i);
                }
            }
            InfoHandler.Instance.Set_SelectItemSlots(m_Slots);
        }

        if (isHoldingSlotParent == true)
        {

            InfoHandler.Instance.Set_HoldingItemSlots(m_Slots);
        }
    }

    public void Set_Image(Image InstantiateImage)
    {
        m_ItemEmage = InstantiateImage.sprite;
    }
    public void Set_Index(int iIndex)
    {
        m_eIndex = (Item.ITEM)iIndex;
    }

    public void Item_Selected()
    {
        if (m_bSelected)
            return;

        if (InfoHandler.Instance.Set_HoldingItem(this) == true)
        {
            Slot_Selected(true);
            //highlighted
        }

    }

    public void Item_UnSelected()//HoldingSlot¿ë
    {
        InfoHandler.Instance.Set_Unholding(iHoldingSlotIndex);
        Have_Items(false);
    }

    public void Slot_Selected(bool bSelected)
    {
        m_bSelected = bSelected;
    }

    public void Have_Items(bool isHave)
    {
        if (isHave)
        {
            m_MyImage.sprite = m_ItemEmage;
            m_MyImage.color = m_Itemcolor;
            m_MyButton.interactable = true;
        }
        else
        {
            m_MyImage.sprite = m_EmptyImage;
            m_MyImage.color = m_Emptycolor;
            m_MyButton.interactable = false;

        }
    }
    public int Get_SlotIndex()
    {
        return m_iSelectSlotIndex;//Select¿ë
    }
    public Image Get_Image()
    {
        return m_MyImage;
    }

    public int Get_Index()
    {
        return (int)m_eIndex;
    }
}
