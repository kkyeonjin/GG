using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _agentmanager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> _agents;

    // Update is called once per frame
    void Update()
    {
        
    }

    void _cal_distance()
    {
        for (int i = 0; i < _agents.Count; i++)
        {
            float d;
            d = Vector3.Distance(_agents[i].transform.position, transform.position);
        }    
    }
}
