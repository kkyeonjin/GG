using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                Cursor.lockState = CursorLockMode.Locked;
              //  cellphone.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
              //  cellphone.SetActive(true);
            }
        }
    }
}
