using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectUI : MonoBehaviour
{
    Image m_CurrImage;

    Image m_EmptyImage;
    Image m_ItemEmage;

    Item.ITEM m_eIndex = Item.ITEM.END;
    public ItemSelectUI[] m_Slots;

    void Start()
    {
        
    }

    public void Set_Image(Image InstantiateImage)
    {
        m_ItemEmage = Instantiate(InstantiateImage);
    }
    public void Set_Index(int iIndex)
    {
        m_eIndex = (Item.ITEM)iIndex;
    }

    public Image Get_Image()
    {
        return m_CurrImage;
    }

    public int Get_Index()
    {
        return (int)m_eIndex;
    }
}
