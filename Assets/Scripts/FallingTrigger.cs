using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject breakable;
    public GameObject fallObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //breakable.GetComponent<BreakableWindow_Apt>().breakWindow();
            fallObj.SetActive(true);
        }
    }
}