using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBtn : MonoBehaviour
{

    public Animator Left, Right;
    public GameObject ElevRoom;

    // Start is called before the first frame update
    public void DoorOpen()
    {
        RoomSet();
        Left.SetBool("Open", true);
        Right.SetBool("Open", true);
        Left.SetBool("Close", false);
        Right.SetBool("Close", false);
        Invoke("DoorClose", 3.0f);
    }

    public void DoorClose()
    {
        Left.SetBool("Close", true);
        Right.SetBool("Close", true);
        Left.SetBool("Open", false);
        Right.SetBool("Open", false);

        if (!Elevator.isRide)
        {
            Invoke("RoomDelete", 1.5f);
        }
    }

    public void RoomSet()
    {
        ElevRoom.SetActive(true);
    }

    public void RoomDelete()
    {
        ElevRoom.SetActive(false);
    }
}