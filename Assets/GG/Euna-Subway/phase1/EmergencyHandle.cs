using UnityEngine;
using System;
using System.Collections;

public class EmergencyHandle : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject cover;
    float doorOffset = 0.65f; //�� ������ z�� ������ ��ȭ. left�� +, right�� -
    bool open = false;

    void turnHandle()
    {
        //�ڵ� ������ ���� ���߱� (����? ���� ���� ������ ���缭 ���� ������ �ð�ó��)
    }

    private void OnTriggerEnter(Collider other)
    {
        //���� �Ÿ� �� ���� �� �ٶ���� �� 
        if (other.gameObject.CompareTag("Player")) 
        {
            if (!open) doorOpen();
            else doorClose();
        }
    }

    void coverOpen()
    {
        
    }

    void doorOpen()
    {
        float t = 0f;

        while (t < doorOffset) //0.4
        { 
            leftDoor.transform.localPosition += new Vector3(0f, 0f, 0.0001f);
            rightDoor.transform.localPosition -= new Vector3(0f, 0f, 0.0001f);
            t += 0.000001f;
        }
        open = true;
        Debug.Log("Door Open");
    }

    void doorClose()
    {
        float t = 0f;

        while (t < doorOffset) //0.4
        {
            leftDoor.transform.localPosition -= new Vector3(0f, 0f, 0.0001f);
            rightDoor.transform.localPosition += new Vector3(0f, 0f, 0.0001f);
            t += 0.000001f;
        }
        open = false;
        Debug.Log("Door Close");
    }
}