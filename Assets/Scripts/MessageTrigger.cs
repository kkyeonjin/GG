using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public int triggerNum;
    public GameObject FakeMsgPopUp, FakeMsg;
    public GameObject MomMsgPopUp, MomMsg;
    public bool isSent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && isSent == false)
        {
            if(triggerNum == 1)
            {
                FakeMsgPopUp.SetActive(true);
                FakeMsg.SetActive(true);
            }
            if(triggerNum == 2)
            {
                MomMsgPopUp.SetActive(true);
                MomMsg.SetActive(true);
            }
            isSent = true;
        }
    }
}
