using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public const int maxOrder = 100;
    public int orderStatus;

    private void Start()
    {
        orderStatus = maxOrder;
    }

}
