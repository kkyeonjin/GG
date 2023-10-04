using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellPhone : MonoBehaviour
{
    public GameObject cellphone;

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
}
