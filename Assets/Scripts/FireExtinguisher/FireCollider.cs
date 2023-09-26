using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCollider : MonoBehaviour
{
    ParticleSystem fire;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        fire = this.gameObject.GetComponentInChildren<ParticleSystem>();
    }
}
