using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCtr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Door;
    public bool isOpen = false;
    public int doorNum; //1: 거실-안방 2: 안방-서재
    public bool canOpen;


    public void DoorAnimOn()
    {
        DoorCheck();
        if (canOpen)
        {
            if (isOpen)
            {
                DoorClose();
            }
            else
            {
                DoorOpen();
            }
        }
    }
    public void DoorCheck()
    {
        if (doorNum == 1)
        {
            if (PuzzleMgr.instance.passedPuzzle[0] == 0)
            {
                canOpen = true;
            }
        }
        else if (doorNum == 2)
        {
            if (PuzzleMgr.instance.passedPuzzle[1] == 0)
            {
                canOpen = true;
            }
        }
    }

    public void DoorOpen()
    {
        isOpen = true;
        Door.SetBool("Open", true);
        Door.SetBool("Close", false);
    }

    public void DoorClose()
    {
        isOpen = false;
        Door.SetBool("Open", false);
        Door.SetBool("Close", true);
    }
}
