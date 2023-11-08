using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cushion : MonoBehaviour
{
    public static Cushion instance;

    public bool isUsing;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }

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

    
    private void Update()
    {
        if (this.gameObject.GetComponent<PickableItem>().remainder == 0 && Inventory.instance.invScripts[Inventory.instance.activeNum] != null)
        {
            Pause();
            Inventory.instance.invScripts[Inventory.instance.activeNum].disposable = true;
            Inventory.instance.ReArrange();

        }
    }
    
    

}
