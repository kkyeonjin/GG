using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public static bool isRide;
    public static bool isdying;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Ride");
            isRide = true;
            PuzzleMgr.instance.inElevator = true;
            PuzzleMgr.instance.Manual5Unlock();
            Shake.instance.FIrstShake();
            PuzzleMgr.instance.MakeObstacle();
            //other.gameObject.GetComponentInChildren<CharacterStatus>().Set_Damage(other.gameObject.GetComponentInChildren<CharacterStatus>().Get_MaxHP());
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Debug.Log("off");
            isRide = false;
        }
    }

    public void Die()
    {
        isdying = true;
    }

}
