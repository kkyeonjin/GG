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

    private void Update()
    {
        if (!isInRange && !isHolding)
        {
            return;
        }

        if (isInRange && Input.GetKey(KeyCode.R))
        {
            isHolding = true;
            //lockOrderGage = true;
            //Debug.Log("Holding");
            return;
        }
        else
        {
            isHolding = false;
            //Debug.Log("Unhold");
        }

        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            Player = collision.gameObject;
            isInRange = true;
            Debug.Log("in Range");

            
            if(Input.GetKey(KeyCode.R)) 
            {
                Debug.Log("holding bar");
                collision.transform.position = holdingPosition.position;
                collision.transform.rotation = holdingPosition.rotation;
            }
            
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
