using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingBar : MonoBehaviour
{
    private Transform holdingPosition;
    bool isInRange = false;
    public bool isHolding = false;
    GameObject Player;
    //bool lockOrderGage = false;

    private void Start()
    {
        //StartCoroutine(Holding());
        holdingPosition = this.gameObject.transform.Find("HoldingPosition").transform;
    }

    private void Get_KeyInput()
    {
        if (!isInRange) return;
        else if (Input.GetKey(KeyCode.R)) 
        {
            isHolding = true;
            Debug.Log("holding bar");
        }
        else
        {
            isHolding = false;
        }
    }

    private void Update()
    {
        if(Player == null) return;
        Get_KeyInput();
        Player.GetComponentInChildren<Animator>().SetBool("Holding", isHolding);

        /*
        if (isHolding)
        {
            Player.GetComponentInChildren<Animator>().SetBool("Holding", isHolding);
            //lockOrderGage = true;
            Debug.Log("Holding");
            return;
        }
        else
        {
            Player.GetComponentInChildren<Animator>().SetBool("Holding", false);
            Debug.Log("Unhold");
        }
        */

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            Player = collision.gameObject;
            isInRange = true;
            Debug.Log("in Range");

            /*
            if (isHolding)
            {
                collision.transform.position = holdingPosition.position;
                collision.transform.rotation = holdingPosition.rotation;
            }
            */

        }
    }



    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("out of Range");
        }
    }
}
