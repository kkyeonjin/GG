using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Sprite icon; // ȹ�� �� �κ��丮 â�� ��� ������
    public Sprite ItemInfo;
    public int itemNum; // ������ ���� ��ȣ �ο�(1����)

    // Start is called before the first frame update

    public void ItemPick()
    {
        for(int i = 0; i < 3; i++)
        {
            if(Inventory.instance.inventory[i] == 0)
            {
                Inventory.instance.inv[i] = this.gameObject;
                Inventory.instance.inventory[i] = itemNum;
                Inventory.instance.invIcons[i].sprite = icon;

                break;
            }
        }
    }
    
    public void ItemUse()
    {
        Debug.Log("sssssss");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
