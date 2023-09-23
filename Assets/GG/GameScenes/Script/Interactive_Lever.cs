using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Lever : Interactive
{
    // Start is called before the first frame update
    void Start()
    {
        m_eType = INTERACT.LEVER;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interacting(GameObject player)
    {
        base.Interacting(player);
    }


}
