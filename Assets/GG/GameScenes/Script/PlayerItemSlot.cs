using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSlot : MonoBehaviour
{//�÷��̾ ����� ���� ������ �κ��丮 �ʳ����δٰ�
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
