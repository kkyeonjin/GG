using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public List<PickableItem> invScripts = new List<PickableItem>(new PickableItem[3]);
    public List<Image> invIcons = new List<Image>(new Image[3]);
    public int activeNum = 0; // Ȱ��ȭ �κ��丮 �ε���
    public int activeItem; // Ȱ��ȭ ������ ������ȣ
    public Sprite blank;

    
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }

    // Start is called before the first frame update
    void Start()
    {
        activeNum = 2;
    }

    public void ItemChange() // Ȱ��ȭ ������ ����
    {
        int prevNum;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            prevNum = activeNum;
            if(prevNum != 2)
            {
                activeNum++;
            }
            else
            {
                activeNum = 0;
            }
            invIcons[prevNum].GetComponent<Outline>().enabled = false;
            invIcons[activeNum].GetComponent<Outline>().enabled = true;
            if(invScripts[activeNum] != null)
            {
                activeItem = invScripts[activeNum].itemNum;
            }
        }
        else
        {
            if(invScripts[activeNum] != null)
            {
                activeItem = invScripts[activeNum].itemNum;
            }
            else
            {
                activeItem = 0;
            }
        }
    }

    public void ReArrange() // ������ ��� �� ������, �κ��丮 ����Ʈ ������
    {
        
        if( invScripts[activeNum].disposable )
        {
            switch (activeNum)
            {
                case 0:
                    //itemImage rearrange
                    invIcons[0].sprite = invIcons[1].sprite;
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = blank;
                    //inventory rearrange
                    invScripts[0] = invScripts[1];
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                case 1:
                    //itemImage rearrange
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = blank;
                    //inventory rearrange
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                default:
                    //itemImage rearrange
                    invIcons[activeNum].sprite = blank;
                    //inventory rearrange
                    invScripts[activeNum] = null;
                    break;

            }
        }
        
    }

    public void ItemUse()
    {
        if(activeItem != 0 )
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                //������ ���� switch�� �ۼ��ؼ�...
                invScripts[activeNum].ItemUse();
                ReArrange();
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                invScripts[activeNum].ItemPause();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ItemChange();
        //Inv_itemRearrange();
        ItemUse();
    }
}
