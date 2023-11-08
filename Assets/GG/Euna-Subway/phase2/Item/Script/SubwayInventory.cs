using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SubwayInventory : MonoBehaviour
{
    public static SubwayInventory instance;
    public Sprite defaultImage;
    public List<SubwayItem> invScripts = new List<SubwayItem>(new SubwayItem[3]);
    public List<Image> invIcons = new List<Image>(new Image[3]);
    public int selectedNum = 0; // 활성화 인벤토리 인덱스
    public int selectedItem; // 활성화 아이템 고유번호

    public OrderGage orderGage;

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
        selectedNum = 2;
    }

    void Update()
    {
        closeCam();
        Inv_selectItem();
        Inv_itemUse();
    }

    public void Inv_selectItem() // 활성화 아이템 변경
    {
        int prevNum;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            prevNum = selectedNum;
            if (prevNum != 2)
            {
                selectedNum++;
            }
            else
            {
                selectedNum = 0;
            }
            invIcons[prevNum].GetComponent<Outline>().enabled = false;
            invIcons[selectedNum].GetComponent<Outline>().enabled = true;
            if (invScripts[selectedNum] != null)
            {
                selectedItem = invScripts[selectedNum].itemNum;
            }
        }
        else
        {
            if (invScripts[selectedNum] != null)
            {
                selectedItem = invScripts[selectedNum].itemNum;
            }
            else
            {
                selectedItem = 0;
            }
        }
    }

    public void Inv_itemRearrange() // 아이템 사용 시 아이콘, 인벤토리 리스트 재정렬
    {

        if (invScripts[selectedNum].Get_isUsed())
        {
            switch (selectedNum)
            {
                case 0:
                    //itemImage rearrange
                    invIcons[0].sprite = invIcons[1].sprite;
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = defaultImage;
                    //inventory rearrange
                    invScripts[0] = invScripts[1];
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                case 1:
                    //itemImage rearrange
                    invIcons[1].sprite = invIcons[2].sprite;
                    invIcons[2].sprite = defaultImage;
                    //inventory rearrange
                    invScripts[1] = invScripts[2];
                    invScripts[2] = null;
                    break;
                default:
                    //itemImage rearrange
                    invIcons[selectedNum].sprite = defaultImage;
                    //inventory rearrange
                    invScripts[selectedNum] = null;
                    break;

            }
        }

    }

    public void Inv_itemUse()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (selectedItem != 0)
            {
                invScripts[selectedNum].Item_effect();
                Inv_itemRearrange();
                selectedItem = 0;
            }
        }
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
