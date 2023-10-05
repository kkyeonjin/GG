using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SubwayInventory : MonoBehaviour
{
    public static SubwayInventory instance;
    public List<SubwayItems> invScripts = new List<SubwayItems>(new SubwayItems[3]);
    public List<Image> invIcons;
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
        invScripts = new List<SubwayItems>(new SubwayItems[3]);
        
        int a = 10;
        activeNum = 2;
    }

    public void ItemChange() // 활성화 아이템 변경
    {
        int prevNum;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            prevNum = activeNum;
            if (prevNum != 2)
            {
                activeNum++;
            }
            else
            {
                activeNum = 0;
            }
            invIcons[prevNum].GetComponent<Outline>().enabled = false;
            invIcons[activeNum].GetComponent<Outline>().enabled = true;
            if (invScripts[activeNum] != null)
            {
                activeItem = invScripts[activeNum].itemNum;
            }
        }
        else
        {
            if (invScripts[activeNum] != null)
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

        if (invScripts[activeNum].disposable)
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
        if (activeItem != 0)
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
        closeCam();
        ItemChange();
        //ReArrange();
        ItemUse();
    }

    //Phase1 비상 레버 & 플래시 closeCam 사용시
    private void closeCam()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("click " + hit.collider.name);
                if(hit.collider.name == "LeverPoint" && EmergencyLever.leverCamActivated)
                {
                    Debug.Log("hit lever");
                    LeverBar lever = hit.collider.gameObject.GetComponentInParent<LeverBar>();
                    
                    lever.add_clickCount();
                    lever.turn_lever();
                    lever.check_ifClear();
                    return;
                }

                if(hit.collider.name == "Flashlight" && FlashlightArea.flashCamActivated)
                {
                    Flashlight flashlight = hit.collider.gameObject.GetComponent<Flashlight>();
                    flashlight.pickUp();
                    return;
                }
            }
        }
    }
}
