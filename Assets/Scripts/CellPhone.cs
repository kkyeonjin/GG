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

            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            //  cellphone.SetActive(false);

            else if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;

            // Cursor.lockState = CursorLockMode.None;
            //  cellphone.SetActive(true);
        
        }
    }
}
