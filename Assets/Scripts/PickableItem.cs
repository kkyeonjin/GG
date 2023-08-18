using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Sprite icon; // 획득 시 인벤토리 창에 띄울 아이콘
    public Sprite ItemInfo;
    public int itemNum; // 아이템 고유 번호 부여(1부터)

    // Start is called before the first frame update

    public void ItemPick()
    {
        for(int i = 0; i < 3; i++)
        {
            if(Inventory.instance.inventory[i] == 0)
            {
                Inventory.instance.inventory[i] = itemNum;
                Inventory.instance.invIcons[i].sprite = icon;

                break;
            }
        }
    }
    
    public void ItemUse()
    {
        switch(itemNum)
        {
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
