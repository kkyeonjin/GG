using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellPhone : MonoBehaviour
{
    public GameObject cellphone;

    private void Update()
    {
        PhoneKey();
    }

    public void Click()
    {
        if(cellphone.activeSelf)
        {
            cellphone.SetActive(false);
        }
        else
        {
            cellphone.SetActive(true);
        }
    }

    public void PhoneKey()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(cellphone.activeSelf)
            {
                cellphone.SetActive(false);
            }
            else
            {
                cellphone.SetActive(true);
            }
        }
    }
}
