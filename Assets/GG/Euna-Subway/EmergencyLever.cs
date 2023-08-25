using UnityEngine;
using System;
using System.Collections;

public class EmergencyLever : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    float doorOffset = 0.65f; //�� ������ z�� ������ ��ȭ. left�� +, right�� -
    bool open = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (!open) doorOpen();
            else doorClose();
        }
    }

    void doorOpen()
    {
        float t = 0.01f;

        while (t< doorOffset) //0.4
        {
            leftDoor.transform.position += new Vector3(0f, 0f, t);
            rightDoor.transform.position -= new Vector3(0f, 0f, t);
        }

        Debug.Log("Door Open");
    }

    void doorClose()
    {
        float t = 0.01f;

        while (t < doorOffset) //0.4
        {
            leftDoor.transform.position -= new Vector3(0f, 0f, t);
            rightDoor.transform.position += new Vector3(0f, 0f, t);
        }

        Debug.Log("Door Close");

    }


}