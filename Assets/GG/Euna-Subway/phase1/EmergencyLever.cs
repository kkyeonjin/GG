using UnityEngine;
using System;
using System.Collections;

public class EmergencyLever : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject cover;
    private Camera leverCam;
    float doorOffset = 0.65f; //문 열림시 z축 포지션 변화. left는 +, right는 -
    bool open = false;

    private void Awake()
    {
        leverCam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Earthquake.isQuake)
        {
            
        }
    }

    void turnHandle()
    {
        //핸들 돌리는 방향 맞추기 (퀴즈? 현재 지진 강도에 맞춰서 레버 돌리기 시계처럼)
    }

    private void OnTriggerEnter(Collider other)
    {
        //상호작용 E
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //카메라 전환 (PlayerCam -> leverCam)
                leverCam.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //카메라 전환 (leverCam-> PlayerCam)
            leverCam.gameObject.SetActive(false);
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