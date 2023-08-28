using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingBar : MonoBehaviour
{
    bool isInRange = false;
    public bool isHolding = false;
    GameObject Player;
    //bool lockOrderGage = false;

    private void Start()
    {
        //StartCoroutine(Holding());
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            Player = collision.gameObject;
            isInRange = true;
            //Debug.Log("in Range");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            //Debug.Log("out of Range");
        }
    }
}
