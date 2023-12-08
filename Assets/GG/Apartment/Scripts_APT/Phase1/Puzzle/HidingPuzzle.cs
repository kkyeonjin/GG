using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HidingPuzzle : MonoBehaviour
{
    public static HidingPuzzle instance;
    // Start is called before the first frame update
    public GameObject msgPop;
    public GameObject msg;
    public GameObject msgList1, msgList2;

    public AudioSource msgAudio;

    //public GameObject msg;
    public bool isFirst = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }
    public void HidePuzzle()
    {
        //PuzzleMgr.instance.Manual3Unlock();
        PuzzleMgr.instance.passedPuzzle[2] = 0;
    }

    public void False()
    {

    }

    public void MsgPop()
    {
        Debug.Log("msgPop");
        msgAudio.Play();
        msgPop.SetActive(true);
        msg.SetActive(true);
        msgList1.SetActive(false);
        msgList2.SetActive(true);
        Invoke("MsgClose", 4.0f);
    }

    public void MsgClose()
    {
        msgAudio.Pause();
        msgPop.SetActive(false);
    }
}
