using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SubwayInventory : MonoBehaviour
{

    public static SubwayInventory instance;
    public List<SubwayItems> invScripts = new List<SubwayItems>(new SubwayItems[3]);
    public List<Image> invIcons = new List<Image>(new Image[3]);
    public int activeNum = 0; // 활성화 인벤토리 인덱스
    public int activeItem; // 활성화 아이템 고유번호

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("Subway inventory set");
        }
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

    public void ReArrange() // 아이템 사용 시 아이콘, 인벤토리 리스트 재정렬
    {
        
        if( invScripts[activeNum].disposable )
        {
            switch (activeNum)
            {
                case 0:
                    //icon rearrange
                    invIcons[0].sprite = invIcons[1].sprite;
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = null;
                    //inventory rearrange
                    invScripts[0] = invScripts[1];
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                case 1:
                    //icon rearrange
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = null;
                    //inventory rearrange
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                default:
                    //icon rearrange
                    invIcons[activeNum].sprite = null;
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
                //아이템 별로 switch문 작성해서...
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
        //ReArrange();
        ItemUse();
    }
}
