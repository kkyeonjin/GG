using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEX : MonoBehaviour
{
    public GameObject FireParticle;

    public void Jet()
    {
            FireParticle.SetActive(true);
            FireParticle.GetComponent<ParticleSystem>().Play();
    }

    public void Pause()
    {
        FireParticle.SetActive(false);
        FireParticle.GetComponent<ParticleSystem>().Stop();
    }
}
