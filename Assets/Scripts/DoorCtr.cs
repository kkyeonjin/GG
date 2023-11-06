using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class DoorCtr : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Door;
    public bool isOpen = false;
    public int doorNum; //1: 거실-안방 2: 안방-서재
    public bool canOpen;

    public float time;
    private float curTime;
    //public TMP_Text timeText;
    //public GameObject Timer;
    public bool isTimerOn;
    public float second;
    public GameObject TimerObject;
    public Image Timer;
    public TMP_Text countText;

    public int count;

    public void DoorAnimOn()
    {
        DoorCheck();
        if (canOpen)
        {
            if (isOpen)
            {
                DoorClose();
            }
            else
            {
                DoorOpen();
            }
        }
    }
    public void DoorCheck()
    {
        if (doorNum == 1)
        {
            if (PuzzleMgr.instance.passedPuzzle[0] == 0)
            {
                canOpen = true;
            }
        }
        else if (doorNum == 2)
        {
            if (PuzzleMgr.instance.passedPuzzle[1] == 0)
            {
                canOpen = true;
            }
        }
    }

    public void DoorOpen()
    {
        isOpen = true;
        Door.SetBool("Open", true);
        Door.SetBool("Close", false);
    }

    public void DoorClose()
    {
        isOpen = false;
        Door.SetBool("Open", false);
        Door.SetBool("Close", true);
    }

    public void DoorTimerStart()
    {
        if(isTimerOn == false && PuzzleMgr.instance.playingPhase != 1 && isOpen == false)
        {
            count = 0;
            TimerObject.gameObject.SetActive(true);
            StartCoroutine(DoorTimer());
            isTimerOn = true;
        }
        else if( isTimerOn == true && PuzzleMgr.instance.playingPhase != 1 )
        {
            return;
        }
        else
        {
            DoorAnimOn();
        }
    }

    public void CountUp()
    {
        if(isTimerOn == true)
        {
            count++;
            countText.text = count.ToString() + "/ 10";
            if (count == 10)
            {
                DoorAnimOn();
                isTimerOn = false;
                //count = 0;
                TimerObject.gameObject.SetActive(false);
            }
        }
    }
    IEnumerator DoorTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            second = (int)curTime % 60;
            Timer.fillAmount = Mathf.Lerp(Timer.fillAmount, (time - curTime)/time, time - curTime );
            //timeText.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime <= 0)
            {
                //Timer.gameObject.SetActive(false);
                Debug.Log("시간 종료");
                curTime = 0;
                TimerObject.gameObject.SetActive(false);
                isTimerOn = false;
                yield break;
            }
        }
    }
}
