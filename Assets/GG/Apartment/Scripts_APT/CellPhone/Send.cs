using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send : MonoBehaviour
{
    public GameObject msg;
    public void SendMsg()
    {
        msg.SetActive(true);
        
    }
}
