using UnityEngine;
using System;
using System.Collections;
using Cinemachine;

public class EmergencyLever : MonoBehaviour
{
    public CinemachineVirtualCamera closeCam;
    public static bool leverCamActivated = false;
    public GameObject cover;

    public GameObject leftDoor;
    public GameObject rightDoor;
    float doorOffset = 0.65f; //문 열림시 z축 포지션 변화. left는 +, right는 -
    bool open = false;

    public GameObject PressE;

    private void Awake()
    {
        
        if (closeCam = transform.Find("LeverCam").GetComponent<CinemachineVirtualCamera>())
        {
            Debug.Log("LeverBar cam set");
        };
        
    }

    private void Start()
    {

    }

    private void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressE.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Phase1Mgr.Instance.earthquake.isQuakeStop)
        {
            //상호작용 E
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PressE.SetActive(false);
                    GameMgr.Instance.FollowCamera.gameObject.SetActive(false);
                    closeCam.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;

                    leverCamActivated = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressE.SetActive(false);
            GameMgr.Instance.FollowCamera.gameObject.SetActive(true);
            closeCam.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            leverCamActivated = false;
        }
    }

    public void doorOpen()
    {

        leftDoor.transform.localPosition += new Vector3(0f, 0f, doorOffset);
        rightDoor.transform.localPosition -= new Vector3(0f, 0f, doorOffset);
        
        //float t = 0f;
        /*
        while (t < doorOffset) //0.4
        { 
            leftDoor.transform.localPosition += new Vector3(0f, 0f, 0.0001f);
            rightDoor.transform.localPosition -= new Vector3(0f, 0f, 0.0001f);
            t += 0.000001f;
        }
        */

        open = true;
        Debug.Log("Door Open");
    }

    void doorClose()
    {
        leftDoor.transform.localPosition -= new Vector3(0f, 0f, doorOffset);
        rightDoor.transform.localPosition += new Vector3(0f, 0f, doorOffset);
        
        /*
        float t = 0f;

        while (t < doorOffset) //0.4
        {
            leftDoor.transform.localPosition -= new Vector3(0f, 0f, 0.0001f);
            rightDoor.transform.localPosition += new Vector3(0f, 0f, 0.0001f);
            t += 0.000001f;
        }
        */
        open = false;
        Debug.Log("Door Close");
    }
}