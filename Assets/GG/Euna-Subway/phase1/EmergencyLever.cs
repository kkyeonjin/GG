using UnityEngine;
using System;
using System.Collections;

public class EmergencyLever : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject cover;
    private Camera leverCam;
    float doorOffset = 0.65f; //�� ������ z�� ������ ��ȭ. left�� +, right�� -
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
        //�ڵ� ������ ���� ���߱� (����? ���� ���� ������ ���缭 ���� ������ �ð�ó��)
    }

    private void OnTriggerEnter(Collider other)
    {
        //��ȣ�ۿ� E
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //ī�޶� ��ȯ (PlayerCam -> leverCam)
                leverCam.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ī�޶� ��ȯ (leverCam-> PlayerCam)
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