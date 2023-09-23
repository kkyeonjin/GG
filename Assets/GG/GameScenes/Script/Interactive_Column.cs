using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Column : Interactive
{
    // Start is called before the first frame update
    void Start()
    {
        m_eType = INTERACT.COLUMN;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interacting(GameObject player)
    {
        Vector3 vObjectpos = transform.position;
        vObjectpos.y = player.transform.position.y;

        player.transform.LookAt(vObjectpos);

        Vector3 vPlayerpos = transform.position - player.transform.forward * 9f;
        vPlayerpos.y = player.transform.position.y;
        player.transform.position = vPlayerpos;
    }

}
