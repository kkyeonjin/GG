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
        //Fire Emission을 0까지 점점 줄이고
        //Collider 삭제
        int t = other.gameObject.GetComponent<FireParticle>().count++;
        Debug.Log(other.gameObject.GetComponent<FireParticle>().count);
        //Debug.Log("!!!");
        fire = other.gameObject.GetComponentInChildren<ParticleSystem>();
        smoke = fire.gameObject.GetComponentInChildren<ParticleSystem>();
        var fire_em = fire.emission;
        var smoek_em = smoke.emission;
        fire_em.enabled = true;
        smoek_em.enabled = true;

        if(t >= 110)
        {
            Debug.Log("!!!");
            fire_em.rateOverTime = Mathf.Lerp(100.0f, 0.0f, t * 5f);
            smoek_em.rateOverTime = Mathf.Lerp(5.0f, 0.0f, t * 5f);
        } 

    }

    

}
