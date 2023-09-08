using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEX : MonoBehaviour
{
    public GameObject FireParticle;
    public int remainder = 40; 

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
        remainder--;
    }

    private void Update()
    {
        if (remainder == 0 && Inventory.instance.invScripts[Inventory.instance.activeNum] != null)
        {
            Pause();
            Inventory.instance.invScripts[Inventory.instance.activeNum].disposable = true;
            Inventory.instance.ReArrange();
            
        }
    }
}
