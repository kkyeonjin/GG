using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEX : MonoBehaviour
{
    public GameObject FireParticle;
    AudioSource FireExSound;
    float remainder;

    private void Awake()
    {
        FireParticle = GameObject.Find("SingleOrigin").transform.GetChild(2).transform.GetChild(0).gameObject;
        FireExSound = this.GetComponent<AudioSource>();
        remainder = this.gameObject.GetComponent<PickableItem>().remainder;
        //public int remainder = 40; 
    }
    public void Jet()
    {
        FireParticle.SetActive(true);
        FireParticle.GetComponent<ParticleSystem>().Play();
        FireExSound.Play();
        InvokeRepeating("Using", 1f, 1f);
    }

    public void Pause()
    {
        CancelInvoke("Using");
        FireParticle.SetActive(false);
        FireParticle.GetComponent<ParticleSystem>().Stop();
        FireExSound.Pause();
    }

    public void Using()
    {
        this.gameObject.GetComponent<PickableItem>().remainder--;
        Inventory.instance.remainderBar[Inventory.instance.activeNum].fillAmount = this.gameObject.GetComponent<PickableItem>().remainder / remainder;
       Debug.Log(this.gameObject.GetComponent<PickableItem>().remainder);
       //ebug.Log(this.gameObject.GetComponent<PickableItem>().remainder / remainder);
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
