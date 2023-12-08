using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Sprite icon; // ȹ�� �� �κ��丮 â�� ��� ������
    public Sprite ItemInfo;
    public int itemNum; // ������ ���� ��ȣ �ο�(1����)
    public bool disposable;
    public float remainder;
    

    // Start is called before the first frame update

    public void ItemPick()
    {
        for(int i = 0; i < 3; i++)
        {
            if(Inventory.instance.invScripts[i] == null)
            {
                Debug.Log("Pick");
                Inventory.instance.invScripts[i] = this.gameObject.GetComponent<PickableItem>();
                Inventory.instance.invIcons[i].sprite = icon;

                break;
            }
        }
        if(itemNum == 4)
        {
            PuzzleMgr.instance.Manual4Unlock();
        }
    }

    public void RemainderBar()
    {

    }    

    public void ItemUse()
    {
        switch(itemNum)
        {
            case 3:
                this.gameObject.GetComponent<Cushion>().UseCushion();
                break;
            case 4:
                this.gameObject.GetComponent<FireEX>().Jet();
                break;
            default:
                break;
        }
        Inventory.instance.remainderBar[Inventory.instance.activeNum].gameObject.SetActive(true);
    }

    public void ItemPause() //������ ȿ�� ����(�������)
    {
        switch(itemNum)
        {
            case 3:
                this.gameObject.GetComponent<Cushion>().Pause();
                break;
            case 4:
                this.gameObject.GetComponent<FireEX>().Pause();
                break;
            default:
                break;
        }
        Inventory.instance.remainderBar[Inventory.instance.activeNum].gameObject.SetActive(false);
    }
    // Update is called once per frame
    
}
