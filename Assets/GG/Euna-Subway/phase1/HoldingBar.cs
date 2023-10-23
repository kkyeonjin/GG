using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingBar : MonoBehaviour
{
    private Transform holdingPosition;
    bool isInRange = false;
    public bool isHolding = false; //애니메이션용 불리언
    Player player;
    bool lockOrderGage = false;

    private void Start()
    {
        //StartCoroutine(Holding());
        holdingPosition = this.gameObject.transform.Find("HoldingPosition").transform;
    }

    private void holdBar()
    {
        if (!isInRange) return;
        else if (Input.GetKey(KeyCode.R)) 
        {
            Phase1Mgr.Instance.isHoldingBar = true;
            isHolding = true;
            Debug.Log("holding bar");
        }
        else
        {
            Phase1Mgr.Instance.isHoldingBar = false;
            isHolding = false;
        }
    }

    private void Update()
    {        
        holdBar();
        player.GetComponentInChildren<Animator>().SetBool("Holding", isHolding);

        if (Phase1Mgr.Instance.earthquake.isQuake)
        {
            //지진 중 player가 붙잡고 있지 않으면 
            if (!isHolding)
            {
                
            }
        }

        /*
        if (isHolding)
        {
            player.GetComponentInChildren<Animator>().SetBool("Holding", isHolding);
            //lockOrderGage = true;
            Debug.Log("Holding");
            return;
        }
        else
        {
            player.GetComponentInChildren<Animator>().SetBool("Holding", false);
            Debug.Log("Unhold");
        }
        */

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {   
            player = collision.gameObject.GetComponent<Player>();
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
        if (collision.gameObject.CompareTag("player"))
        {
            isInRange = false;
            Debug.Log("out of Range");
        }
    }
}
