using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Sprite icon; // ȹ�� �� �κ��丮 â�� ��� ������
    public Sprite ItemInfo;
    public int itemNum; // ������ ���� ��ȣ �ο�(1����)
    public bool disposable;

    // Start is called before the first frame update

    public void ItemPick()
    {
        for(int i = 0; i < 3; i++)
        {
            if(Inventory.instance.invNums[i] == 0)
            {
                Inventory.instance.invScripts[i] = this.gameObject.GetComponent<PickableItem>();
                Inventory.instance.invNums[i] = itemNum;
                Inventory.instance.invIcons[i].sprite = icon;

                break;
            }
        }
    }
    
    public void ItemUse()
    {
        switch(itemNum)
        {
            case 4:
                this.gameObject.GetComponent<FireEX>().Jet();
                break;
            default:
                break;
        }
    }

    public void ItemPause() //������ ȿ�� ����(�������)
    {
        switch(itemNum)
        {
            case 4:
                this.gameObject.GetComponent<FireEX>().Pause();
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
