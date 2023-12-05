using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public enum Effect { Resume, Death, Recover, Invincible, Adrenaline};

    public ParticleSystem[] ParticleEffect;

    private void Start()
    {
        foreach(ParticleSystem p in ParticleEffect)
        {
            p.Stop();
        }
    }


    public void Activate_Particle(Effect index, bool bActive)
    {
        ParticleEffect[(int)index].gameObject.SetActive(bActive);
    }

    public void Play_Particle(Effect index)
    {
        ParticleEffect[(int)index].Play();
        if(index == Effect.Recover)
        {
            Invoke("Stop_Recover", 1f);
        }
        else if(index == Effect.Resume)
        {
            Invoke("Stop_Resume", 1f);
        }
    }

    public void Stop_Particle(Effect index)
    {
        ParticleEffect[(int)index].Stop();
    }
    
    private void Stop_Recover()
    {
        ParticleEffect[(int)Effect.Recover].Stop();
    }
    private void Stop_Resume()
    {
        ParticleEffect[(int)Effect.Resume].Stop();
    }
}
