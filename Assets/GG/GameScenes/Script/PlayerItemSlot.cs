using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSlot : MonoBehaviour
{//플레이어가 사용할 실제 아이템 인벤토리 너낌으로다가
    public Item[] m_Slots;

    private void Start()
    {
        int[,] HoldingItems = InfoHandler.Instance.Get_HoldingItems();

        for(int i=0;i<2;++i)
        {
            
        }
        
    }
    public void Use_Item(int iIndex)
    {
        //m_Slots[iIndex].UseItem();
    }

    public void Set_Item()
    {

    }
}
