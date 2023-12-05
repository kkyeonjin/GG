using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject point;

    // Start is called before the first frame update
    void Start()
    {
        SingleGameMgr.Instance.m_LocalPlayerObj.transform.position = point.transform.position;
    }

    private void OnDestroy()
    {
        Destroy(SingleGameMgr.Instance.m_LocalPlayerObj);
        Destroy(SingleGameMgr.Instance.Canvas);
        Destroy(SingleGameMgr.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
