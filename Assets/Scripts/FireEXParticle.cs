using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEXParticle : MonoBehaviour
{
    ParticleSystem fire;

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        //Fire Emission�� 0���� ���� ���̰�
        //Collider ����
        Debug.Log("!!!");
        fire = other.gameObject.GetComponentInChildren<ParticleSystem>();
        var em = fire.emission;
        em.enabled = true;
        em.rateOverTime = Mathf.Lerp(50.0f, 0.0f, Time.time * 1.5f);
        Debug.Log(fire.emission);

        //other.gameObject.GetComponent<Collider>().enabled = false;
        
    }


}
