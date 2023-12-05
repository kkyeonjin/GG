using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public GameObject news;

    public void NewsPopUp()
    {
        news.SetActive(true);
        Invoke("Off", 3f);
    }

    public void Off()
    {
        news.SetActive(false);
    }
}
