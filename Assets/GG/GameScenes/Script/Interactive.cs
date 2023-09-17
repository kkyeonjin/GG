using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public Transform AdjustPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interacting(GameObject player)
    {
        player.transform.localRotation = AdjustPosition.localRotation;
        player.transform.position = AdjustPosition.position;
    }
}
