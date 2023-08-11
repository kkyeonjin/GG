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


    Item.ITEM m_eIndex = Item.ITEM.END;

    public bool isSlotsParent = false;
    public ItemSelectUI[] m_Slots;

    private void Awake()
    {
        m_EmptyImage= m_MyImage.sprite;
        m_Emptycolor = m_MyImage.color;

        m_Itemcolor = new Color(1f, 1f, 1f, 1f);
    }
    void Start()
    {
        
        if(isSlotsParent)
        {
            int iSlotIndex = 0;
            for(int i=0;i<5;++i)
            {
                if(InfoHandler.Instance.Get_Item_Num(i) > 0)
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

    private void OnCollisionEnter2D(Collision collision)
    {
        Debug.Log("함수 돎?");
        if (this.gameObject.tag == "UISelectItem")
        {
            if (collision.gameObject.tag == "UIHoldingItem")
            {
                //아이템 슬롯 설정
                ItemSelectUI HoldingSlot = collision.gameObject.GetComponent<ItemSelectUI>();
                HoldingSlot.Set_Image(this.m_MyImage);
                HoldingSlot.Set_Index((int)this.m_eIndex);
                HoldingSlot.Have_Items(true);
                Debug.Log("충돌 함?");
            }
        }
   
    }
}
