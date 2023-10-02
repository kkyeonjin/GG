using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCtr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Door;
    public bool isOpen = false;

    public void DoorAnimOn()
    {
        if(isOpen)
        {
            DoorClose();
        }
        else
        {
            DoorOpen(); 
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
