using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Potion: StoreItem
{

    public StoreItem_Potion() : base()
    {
        m_eIndex = ITEM.POSTION;
        Debug.Log("Potion!");

    }
    ~StoreItem_Potion()
    {
        Debug.Log("»ç¶óÁü!");
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        m_eIndex = ITEM.POSTION;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Recover_Potion();
    }
}
