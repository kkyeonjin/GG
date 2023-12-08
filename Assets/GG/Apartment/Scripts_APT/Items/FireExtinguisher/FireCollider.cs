using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggerEnter");
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<CharacterStatus>().Set_Damage(5);
        }
    }
}
