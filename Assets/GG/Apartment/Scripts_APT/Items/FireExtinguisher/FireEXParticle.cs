using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEXParticle : MonoBehaviour
{
    public ParticleSystem fire, smoke;
    public int count;

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Oncollision");
        int t = other.gameObject.GetComponent<FireParticle>().count++;
        fire = other.gameObject.GetComponentInChildren<ParticleSystem>();
        smoke = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        var fire_em = fire.emission;
        var smoek_em = smoke.emission;
        fire_em.enabled = true;
        smoek_em.enabled = true;

        if (t >= 110)
        {
            //Debug.Log("!!!");
            fire_em.rateOverTime = Mathf.Lerp(100.0f, 0.0f, t * 5f);
            smoek_em.rateOverTime = Mathf.Lerp(5.0f, 0.0f, t * 5f);
        }
    }
}
