using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Death : StoreItem
{
    // Start is called before the first frame update
    void Start()
    {
        m_eIndex = ITEM.DEATH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.BroadCast_Death();//게임 메니저 PV로 모든 플레이어에게 전송
    }
}
