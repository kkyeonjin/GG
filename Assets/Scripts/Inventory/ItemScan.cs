using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    public GameObject HidingCam, MainCam;
    //public TMP_Text pressText;
    //Item 용도 설명 UI
    public Image ItemInfo;
    public GameObject pickedItem;

    //Press E UI
    public TMP_Text pressE;

    //HideCam
    public GameObject backbtn;
    public GameObject msgPop;
    public GameObject msg;

    [SerializeField]
    float range;
    RaycastHit hitInfo;

    [SerializeField]
    LayerMask layerMask;

    private void Update()
    {
        CheckItem();
    }

    public void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag("Item"))
            {
                ItemInfo.sprite = hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemInfo;
                ItemInfo.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                    hitInfo.transform.gameObject.transform.SetParent(pickedItem.transform);
                    hitInfo.transform.gameObject.SetActive(false);
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Button"))
            {
                pressE.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Open");
                    hitInfo.transform.gameObject.GetComponent<ElevatorBtn>().DoorOpen();

                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Door"))
            {
                pressE.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<DoorCtr>().DoorAnimOn();
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("HidingTable"))
            {
                pressE.gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //MainCam.gameObject.SetActive(false);
                    HidingCam.gameObject.SetActive(true);
                    PuzzleMgr.instance.passedPuzzle[2] = 0;
                    Invoke("MsgPop", 2.0f);
                    backbtn.SetActive(true);
                    msg.SetActive(true);

                    /*
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        HidingCam.gameObject.SetActive(false);
                        MainCam.gameObject.SetActive(true);
                        PuzzleMgr.instance.passedPuzzle[2] = false;
                    }*/
                    //미션 성공 여부 bool 변수 true로 바꿔주기
                }
            }
            else if(hitInfo.transform.gameObject.CompareTag("FalseTable"))
            {
                pressE.gameObject.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    //숨는 애니메이션
                }
            }
        }
        else
        {
            ItemInfo.gameObject.SetActive(false);
            pressE.gameObject.SetActive(false);
        }
    }

    public void MsgPop()
    {
        msgPop.SetActive(true);
        Invoke("MsgClose", 4.0f);
    }

    public void MsgClose()
    {
        msgPop.SetActive(false);
    }
}