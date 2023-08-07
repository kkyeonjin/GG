using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Sprite icon;
    public int itemNum;


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

    public void IconUpdate()
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
