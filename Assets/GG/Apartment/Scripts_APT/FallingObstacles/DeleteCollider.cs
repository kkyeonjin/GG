using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollider : MonoBehaviour
{
    private void Start()
    {
        Invoke("ChangeTag", 3.5f);
    }

    public void ChangeTag()
    {
        this.gameObject.tag = "Untagged";
    }
}
