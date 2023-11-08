using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cushion : MonoBehaviour
{
    public GameObject usingCushionUI;
    public bool isUsing;
    // Start is called before the first frame update
    public void CushionDamage()
    {
        if(isUsing)
        {
            this.gameObject.GetComponent<PickableItem>().remainder -= 1;
        }
    }
    public void Pause()
    {
        isUsing = false;
    }
    public void UseCushion()
    {
        isUsing = true;
    }


}
