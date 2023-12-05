using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSwitch : MonoBehaviour
{
    public GameObject sw1, sw2, sw3, sw4, sw5;
    public bool isFirst = true;
    public void TurnOff()
    {
        sw1.transform.localEulerAngles = new Vector3(14, 0, 0);
        sw2.transform.localEulerAngles = new Vector3(14, 0, 0);
        sw3.transform.localEulerAngles = new Vector3(14, 0, 0);
        sw4.transform.localEulerAngles = new Vector3(14, 0, 0);
        sw5.transform.localEulerAngles = new Vector3(14, 0, 0);
        isFirst = false;

        if(isFirst)
        {
            //매뉴얼해금
        }     
    }
}