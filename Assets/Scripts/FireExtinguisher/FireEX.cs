using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEX : MonoBehaviour
{
    public GameObject FireParticle;

    private void Awake()
    {
        FireParticle = GameObject.Find("SingleOrigin").transform.GetChild(2).transform.GetChild(0).gameObject;
        //public int remainder = 40; 
    }
    public void Jet()
    {
        FireParticle.SetActive(true);
        FireParticle.GetComponent<ParticleSystem>().Play();
        InvokeRepeating("Using", 1f, 1f);
    }

    public void Pause()
    {
        CancelInvoke("Using");
        FireParticle.SetActive(false);
        FireParticle.GetComponent<ParticleSystem>().Stop();
    }

    public void Using()
    {
        this.gameObject.GetComponent<PickableItem>().remainder--;
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
