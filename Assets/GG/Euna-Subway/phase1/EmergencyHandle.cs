using UnityEngine;
using System;
using System.Collections;

public class EmergencyHandle : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public GameObject cover;
    float doorOffset = 0.65f; //문 열림시 z축 포지션 변화. left는 +, right는 -
    bool open = false;

    void turnHandle()
    {
        //핸들 돌리는 방향 맞추기 (퀴즈? 현재 지진 강도에 맞춰서 레버 돌리기 시계처럼)
    }

    private void OnTriggerEnter(Collider other)
    {
        //일정 거리 내 접근 후 바라봤을 때 
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