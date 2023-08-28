using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEXParticle : MonoBehaviour
{
    ParticleSystem fire;
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
        var em = fire.emission;
        em.enabled = true;

        if(t >= 110)
        {
            Debug.Log("!!!");
            em.rateOverTime = Mathf.Lerp(100.0f, 0.0f, t * 5f);
        } 

    }

    

}
