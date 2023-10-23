using UnityEngine;
using System;
using System.Collections;
using Cinemachine;

public class EmergencyLever : MonoBehaviour
{
    private CinemachineVirtualCamera closeCam;
    public static bool leverCamActivated = false;
    public GameObject cover;

    public GameObject leftDoor;
    public GameObject rightDoor;
    float doorOffset = 0.65f; //문 열림시 z축 포지션 변화. left는 +, right는 -
    bool open = false;

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

    }

    private void OnTriggerStay(Collider other)
    {
        if (Earthquake.isQuakeStop || Earthquake.isQuake)
        {
            //상호작용 E
            if (other.CompareTag("player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //카메라 전환 (PlayerCam -> closeCam)
                    closeCam.gameObject.SetActive(true);
                    leverCamActivated = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            //카메라 전환 (closeCam-> PlayerCam)
            closeCam.gameObject.SetActive(false);
            leverCamActivated = false;
        }
    }

    void coverOpen()
    {
        
    }

    void doorOpen()
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