using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingBar : MonoBehaviour
{
    private Transform holdingPosition;
    public bool isHolding = false; //애니메이션용 불리언
    Player player;

    public GameObject PressE;

    private void Start()
    {
        //StartCoroutine(Holding());
        holdingPosition = this.gameObject.transform.Find("HoldingPosition").transform;
    }
    private void holdBar()
    {
        if (Input.GetKey(KeyCode.E))
        {
            PressE.SetActive(false);

            isHolding = true;
            player.SetAnimation("Holding", isHolding);

            Debug.Log("holding bar");
            if (Phase1Mgr.Instance.earthquake.isQuake)
            {
                Phase1Mgr.Instance.playerIsHoldingBar = true;
                //Phase1Mgr.Instance.Check_Clear(Phase1Mgr.phase1CC.HoldBar);
            }
        }

        else
        {
            isHolding = false;
            player.SetAnimation("Holding", isHolding);

            if (Phase1Mgr.Instance.earthquake.isQuake)
            {
                Phase1Mgr.Instance.playerIsHoldingBar = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PressE.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //지진 중 기둥 Trigger 발동
        if (collision.gameObject.CompareTag("Player"))
        {   
            player = collision.gameObject.GetComponent<Player>();

            holdBar();

            if (isHolding) //기둥 붙잡기 포지션 
            {
                collision.transform.position = holdingPosition.position;
                collision.transform.rotation = holdingPosition.rotation;
            }
        }
    }


    private void OnTriggerExit(Collider collision)
    {
        if (!Phase1Mgr.Instance.earthquake.isQuake) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PressE.SetActive(false);
        }
    }
}
