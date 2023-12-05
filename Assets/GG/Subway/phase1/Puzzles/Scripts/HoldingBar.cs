using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingBar : MonoBehaviour
{
    private Transform holdingPosition;
    public bool isHolding = false; //�ִϸ��̼ǿ� �Ҹ���
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
        //���� �� ��� Trigger �ߵ�
        if (collision.gameObject.CompareTag("Player"))
        {   
            player = collision.gameObject.GetComponent<Player>();

            holdBar();

            if (isHolding) //��� ����� ������ 
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
