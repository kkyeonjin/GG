using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ElevatorCtr : MonoBehaviour
{
    public GameObject ElevRoom;
   
    public void ElevRoomSet()
    {
        ElevRoom.SetActive(true);
    }

    public void ElevRoomDelete()
    {
        ElevRoom.SetActive(false);
    }
}