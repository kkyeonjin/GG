using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public static bool isRide;
    public static bool isdying;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "player")
        {
            //Debug.Log("Ride");
            isRide = true;
            other.gameObject.GetComponentInChildren<CharacterStatus>().Set_Damage(150);

            /*
            Invoke("Die", 2.0f);
            if (isdying)
            {
                other.gameObject.GetComponentInChildren<CharacterStatus>().Set_Damage(150);
            }
            */
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
