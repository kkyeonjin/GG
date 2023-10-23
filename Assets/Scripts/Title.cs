using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject[] chars;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Create", 0, 3f);
    }

    public void Create()
    {
        pos = new Vector3(Random.Range(-640, 640), 460, Random.Range(-150, -80));
        Quaternion rot = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), 0);
        Instantiate(chars[Random.Range(0, chars.Length)], pos, rot);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
