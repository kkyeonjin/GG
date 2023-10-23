using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ElevatorCtr : MonoBehaviour
{
    public GameObject ElevRoom, BackWall;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            ElevRoom.SetActive(true);
            BackWall.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            ElevRoom.SetActive(false);
            BackWall.SetActive(true);
        }
    }
}