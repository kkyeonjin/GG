using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : MonoBehaviour
{
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        //localPosition -> �θ� ���� ������� ��ġ�� 
        transform.localPosition = new Vector3(Random.Range(-1.45f, -0.2f), -1.6f, Random.Range(-1.5f, -0.3f));
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= 140)
        {
            this.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
