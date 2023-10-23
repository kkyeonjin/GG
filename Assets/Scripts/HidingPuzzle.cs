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

    //public GameObject msg;
    public bool isFirst = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }
    public void HidePuzzle()
    {
        Debug.Log("Start");
        if(PuzzleMgr.instance.passedPuzzle[0] == 0 && PuzzleMgr.instance.passedPuzzle[1] == 0 && isFirst == false )
        {
            isFirst = true;
            PuzzleMgr.instance.Manual3Unlock();
            PuzzleMgr.instance.passedPuzzle[2] = 0;
            Invoke("MsgPop", 4f);
            msg.SetActive(true);

            this.gameObject.GetComponent<Shake>().FIrstShake();
        }
    }

    public void MsgPop()
    {
        msgPop.SetActive(true);
        Invoke("MsgClose", 4.0f);
    }

    public void MsgClose()
    {
        msgPop.SetActive(false);
    }
}
