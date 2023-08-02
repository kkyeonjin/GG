using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
    public GameObject valve;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        Spin();
    }

    public void Spin()
    {
        if(count != 11)
        {
            count++;
        }
        else
        {
            count = 0;
        }

        Vector3 valveVector = valve.transform.rotation.eulerAngles;

        switch(count)
        {
            case 0:
                valveVector.x = 360;
                break;
            case 1:
                valveVector.x = 330;
                break;
            case 2:
                valveVector.x = 300;
                break;
            case 3:
                valveVector.x = 270;
                break;
            case 4:
                valveVector.x = 240;
                break;
            case 5:
                valveVector.x = 210;
                break;
            case 6:
                valveVector.x = 180;
                break;
            case 7:
                valveVector.x = 150;
                break;
            case 8:
                valveVector.x = 120;
                break;
            case 9:
                valveVector.x = 90;
                break;
            case 10:
                valveVector.x = 60;
                Debug.Log("Correct");
                break;
            case 11:
                valveVector.x = 30;
                break;
        }
        valveVector.y = 90;
        valveVector.z = 270;
        valve.transform.rotation = Quaternion.Euler(valveVector);
        //Debug.Log(valveVector.x);

    }
}
