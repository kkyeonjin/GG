using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject startPos;
    
    void Start()
    {
        this.transform.position = startPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
