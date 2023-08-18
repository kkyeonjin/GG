using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public List<int> inventory = new List<int>() { 0, 0, 0 }; //item고유 number가 있어 숫자를 배열에 저장
    public List<Image> invIcons = new List<Image>(new Image[3]);
    public int activeNum = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        activeNum = 2;
    }

    public void ItemChange()
    {
        int prevNum;

        if(Input.GetKeyDown(KeyCode.Tab))
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

            
        }
    }

    public void IconUse()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(inventory.Count > 0 )
            {
                Debug.Log("use");

                
                switch (activeNum)
                {
                    case 0:
                        invIcons[0].sprite = invIcons[1].sprite;
                        invIcons[1].sprite = invIcons[2].sprite;
                        invIcons[2].sprite = null;
                        break;
                    case 1:
                        invIcons[1].sprite = invIcons[2].sprite;
                        invIcons[2].sprite = null;
                        break;
                    default:
                        invIcons[activeNum].sprite = null;
                        break;

                }
                
                inventory.RemoveAt(activeNum);
            }
        }
    }

    public void ItemUse()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ItemChange();
        IconUse();
    }
}
