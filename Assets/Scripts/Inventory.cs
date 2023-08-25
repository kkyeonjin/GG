using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    public List<int> inventory = new List<int>() { 0, 0, 0 }; //item고유 number가 있어 숫자를 배열에 저장
    public List<GameObject> inv = new List<GameObject>(new GameObject[3]);
    public List<Image> invIcons = new List<Image>(new Image[3]);
    public int activeNum = 0;
    public int activeItem;

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

    public void ItemChange() // 활성화 아이템 변경
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
            activeItem = inventory[activeNum];

            
        }
    }

    public void ReArrange() // 아이템 사용 시 아이콘, 인벤토리 리스트 재정렬
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(inventory.Count > 0 )
            {
                Debug.Log("use");

                
                switch (activeNum)
                {
                    case 0:
                        //icon rearrange
                        invIcons[0].sprite = invIcons[1].sprite;
                        invIcons[1].sprite = invIcons[2].sprite;
                        invIcons[2].sprite = null;
                        //inventory rearrange
                        inventory[0] = inventory[1];
                        inventory[1] = inventory[2];
                        inventory[2] = 0;
                        break;
                    case 1:
                        //icon rearrange
                        invIcons[1].sprite = invIcons[2].sprite;
                        invIcons[2].sprite = null;
                        //inventory rearrange
                        inventory[1] = inventory[2];
                        inventory[2] = 0;
                        break;
                    default:
                        //icon rearrange
                        invIcons[activeNum].sprite = null;
                        //inventory rearrange
                        inventory[activeNum] = 0;
                        break;

                }
            }
        }
    }

    public void ItemUse()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            inv[activeNum].GetComponent<PickableItem>().ItemUse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ItemChange();
        ReArrange();
        ItemUse();
    }
}
