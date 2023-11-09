using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgPopUp : MonoBehaviour
{
    public GameObject msgPop;
    public AudioSource msgSound;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("MsgPop", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MsgPop()
    {
        msgPop.SetActive(true);
        msgSound.Play();
        PuzzleMgr.instance.TimerStart();
        Invoke("MsgClose", 4.0f);
    }

    public void MsgClose()
    {
        msgPop.SetActive(false);
        msgSound.Pause();
    }

}
