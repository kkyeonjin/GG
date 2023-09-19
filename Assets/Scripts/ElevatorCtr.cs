using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ElevatorCtr : MonoBehaviour
{
    public GameObject ElevRoom, BackWall;

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            ElevRoom.SetActive(true);
            BackWall.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            ElevRoom.SetActive(false);
            BackWall.SetActive(true);
        }
    }
}