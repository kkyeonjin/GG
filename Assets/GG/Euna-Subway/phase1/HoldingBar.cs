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
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            Phase1Mgr.Instance.isHoldingBar = true;
            isHolding = true;
            Debug.Log("holding bar");
            player.SetAnimation("Holding", isHolding);
            Phase1Mgr.Instance.Check_Column();
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            Phase1Mgr.Instance.isHoldingBar = false;
            isHolding = false;
            player.SetAnimation("Holding", isHolding);
        }
    }

    private void Update()
    {        
        holdBar();
        

        if (Phase1Mgr.Instance.earthquake.isQuake)
        {
            //지진 중 player가 붙잡고 있지 않으면 
            if (!isHolding)
            {
                SubwayInventory.instance.orderGage.Cut_Order();
                //Debug.Log("Order Gage : " + SubwayInventory.instance.orderGage.Get_Order());
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
        if (collision.gameObject.CompareTag("Player"))
        {   
            player = collision.gameObject.GetComponent<Player>();
            isInRange = true;
            Debug.Log("in Range");

            if (isHolding)
            {
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
