using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingBar : MonoBehaviour
{
    private Transform holdingPosition;
    public bool isHolding = false; //애니메이션용 불리언
    Player player;

    public Image holdingBarUI;

    private void Start()
    {
        //StartCoroutine(Holding());
        holdingPosition = this.gameObject.transform.Find("HoldingPosition").transform;
    }

    private void holdBar()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            Phase1Mgr.Instance.playerIsHoldingBar = true;
            isHolding = true;
            Debug.Log("holding bar");
            player.SetAnimation("Holding", isHolding);
            Phase1Mgr.Instance.Check_Condition(Phase1Mgr.phase1CC.HoldBar);
        }
        else if(Input.GetKeyUp(KeyCode.R))
        {
            Phase1Mgr.Instance.playerIsHoldingBar = false;
            isHolding = false;
            player.SetAnimation("Holding", isHolding);
        }
    }


    private void OnTriggerStay(Collider collision)
    {
        if (!Phase1Mgr.Instance.earthquake.isQuake) return;

        //지진 중 기둥 Trigger 발동
        if (collision.gameObject.CompareTag("Player"))
        {   
            player = collision.gameObject.GetComponent<Player>();
            holdingBarUI.gameObject.SetActive(true);

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
            holdingBarUI.gameObject.SetActive(false);
        }
    }
}
