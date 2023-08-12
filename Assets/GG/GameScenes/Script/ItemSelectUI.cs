using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{
    public Image m_MyImage;

    Sprite m_ItemEmage;
    Sprite m_EmptyImage;

    Color m_Emptycolor;
    Color m_Itemcolor;

    public int iSlotIndex;

    Item.ITEM m_eIndex = Item.ITEM.END;

    public bool isSlotsParent = false;
    public ItemSelectUI[] m_Slots;

    private void Awake()
    {
        m_EmptyImage = m_MyImage.sprite;
        m_Emptycolor = m_MyImage.color;

        m_Itemcolor = new Color(1f, 1f, 1f, 1f);
    }
    void Start()
    {

        if (isSlotsParent)
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
        if (GameMgr.Instance.Set_HoldingItem(this) == true)
        {
            //highlighted
        }

    }

    public void Item_UnSelected()
    {
        GameMgr.Instance.Set_Unholding(iSlotIndex);
        Have_Items(false);
    }

    public void Have_Items(bool isHave)
    {
        if (isHave)
        {
            m_MyImage.sprite = m_ItemEmage;
            m_MyImage.color = m_Itemcolor;
        }
        else
        {
            m_MyImage.sprite = m_EmptyImage;
            m_MyImage.color = m_Emptycolor;
        }
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
