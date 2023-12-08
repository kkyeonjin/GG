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
    public int selectedNum = 0; // Ȱ��ȭ �κ��丮 �ε���
    public int selectedItem; // Ȱ��ȭ ������ ������ȣ

    public OrderGage orderGage;

    public Image AimMark;
    public GameObject Aim;

    public AudioSource audioSrc;
    public AudioClip tabSound;

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
    
    public void Active_AimPoint(bool bActive)
    {
        Aim.gameObject.SetActive(bActive);
        if(bActive)
        {
            AimMark.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
            Color color = AimMark.color;
            color.a = 50f / 255f;
            AimMark.color = color;
        }
    }

    public void On_Targeted(bool bTarget)
    {
        Color color = AimMark.color;
        color.a = bTarget ? 170f / 255f : 50f / 255f;
        AimMark.color = color;

        if (bTarget)
        {
            AimMark.transform.localScale = Vector3.Lerp(AimMark.transform.localScale, new Vector3(0.8f, 0.8f, 1f), 0.5f);
        } 
        else
        {
            AimMark.transform.localScale = Vector3.Lerp(AimMark.transform.localScale, new Vector3(1.3f, 1.3f, 1f), 0.5f);

        }
    }

    public float Get_OrderGauge()
    {
        return orderGage.Get_Order();
    }
    public void Inv_selectItem() // Ȱ��ȭ ������ ����
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
                audioSrc.Play();
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

    public void Inv_itemRearrange() // ������ ��� �� ������, �κ��丮 ����Ʈ ������
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

    public GameObject PressE;
    //Phase1 ��� ���� & �÷��� closeCam ����
    private void closeCam()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Emergency Lever")
                {
                    PressE.SetActive(true);
                }

                //Debug.Log("click " + hit.collider.name);
                if (hit.collider.name == "LeverPoint" && EmergencyLever.leverCamActivated)
                {
                    //Debug.Log("hit lever");
                    LeverBar lever = hit.collider.gameObject.GetComponentInParent<LeverBar>();
                    lever.add_clickCount();
                    lever.turn_lever();
                    lever.check_ifClear();
                    return;
                }

                else if(hit.collider.name == "Flashlight" && FlashlightArea.flashCamActivated)
                {
                    Flashlight flashlight = hit.collider.gameObject.GetComponent<Flashlight>();
                    flashlight.pickUp();
                    return;
                }
                else
                {
                }
            }
        }
    }
}