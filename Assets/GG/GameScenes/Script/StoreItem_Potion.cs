using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Potion: StoreItem
{
    // Start is called before the first frame update
    void Start()
    {
        m_eIndex = ITEM.POSTION;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Recover_Potion();
    }
}
