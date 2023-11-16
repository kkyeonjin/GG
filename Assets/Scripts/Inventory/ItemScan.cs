using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    //public TMP_Text pressText;
    //Item 용도 설명 UI
    public Image ItemInfo;
    public GameObject pickedItem;

    //Press E UI
    public GameObject pressE;
    public Image AimPointUI;

    //HideCam
    public GameObject backbtn;
    public GameObject msgPop;
    public GameObject msg;
    public GameObject mainCan, hideCan;
    public GameObject HidingCam1, HidingCam2, HidingCam3, MainCam;

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
        Color AimColor = AimPointUI.color;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag("Item"))
            {
                PickableItem hitPickableItem = hitInfo.transform.gameObject.GetComponent<PickableItem>();
                ItemInfo.sprite = hitPickableItem.ItemInfo;
                ItemInfo.gameObject.SetActive(true);
                AimColor.a = 1f;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitPickableItem.ItemPick();

                    hitInfo.transform.gameObject.transform.SetParent(pickedItem.transform);
                    hitInfo.transform.gameObject.layer = 25;
                    hitInfo.transform.gameObject.SetActive(false);
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Button"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Open");
                    hitInfo.transform.gameObject.GetComponent<ElevatorBtn>().DoorOpen();

                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Door"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<DoorCtr>().DoorTimerStart();
                    hitInfo.transform.gameObject.GetComponent<DoorCtr>().CountUp();
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("DoorToOut"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PhaseChangeTrigger>().GoToNextPhase();
                }

            }
            else if (hitInfo.transform.gameObject.CompareTag("HidingTable"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Cursor.lockState = CursorLockMode.None;
                    //MainCam.gameObject.SetActive(false);
                    HidingCam1.gameObject.SetActive(true);
                    PuzzleMgr.instance.activeCam[0] = false;
                    PuzzleMgr.instance.activeCam[1] = true;
                    mainCan.SetActive(false);
                    hideCan.SetActive(true);
                    HidingPuzzle.instance.HidePuzzle();
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("FalseTable"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hitInfo.transform.gameObject.name == "Table_04_C1")
                    {
                        HidingCam2.gameObject.SetActive(true);
                        PuzzleMgr.instance.activeCam[0] = false;
                        PuzzleMgr.instance.activeCam[2] = true;
                        mainCan.SetActive(false);
                        hideCan.SetActive(true);
                    }
                    else if (hitInfo.transform.gameObject.name == "PC_Table2_C2")
                    {
                        HidingCam3.gameObject.SetActive(true);
                        PuzzleMgr.instance.activeCam[0] = false;
                        PuzzleMgr.instance.activeCam[3] = true;
                        mainCan.SetActive(false);
                        hideCan.SetActive(true);
                    }
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Puzzle"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<CanvasChange>().PuzzleOn();
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Book"))
            {
                pressE.gameObject.SetActive(true);
                AimColor.a = 1f;
            }
            else if (hitInfo.transform.gameObject.CompareTag("ElectricSwitch"))
            {
                pressE.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<ElectricSwitch>().TurnOff();
                }
            }
        }
        else
        {
            ItemInfo.gameObject.SetActive(false);
            pressE.gameObject.SetActive(false);
            AimColor.a = 0.4f;
        }
        AimPointUI.color = AimColor;
    }
}