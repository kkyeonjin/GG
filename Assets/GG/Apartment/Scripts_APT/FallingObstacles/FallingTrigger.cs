using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject breakable;
    public GameObject fallObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fallObj.SetActive(true);
            Invoke("ChangeTag", 1.5f);
        }
    }

    public void ChangeTag()
    {
        Transform[] allChildren = fallObj.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.tag = "Untagged";
            //Debug.Log(child.name);
        }
    }
}
