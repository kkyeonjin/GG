using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    public GameObject HowTo;

    private void Update()
    {
        HelpPopUp();
    }

    public void HelpPopUp()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (HowTo.activeSelf)
            {
               // Cursor.lockState = CursorLockMode.Locked;
                HowTo.SetActive(false);
            }
            else
            {
              //  Cursor.lockState = CursorLockMode.None;
                HowTo.SetActive(true);
            }
        }
    }
}
