using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Resume : StoreItem
{
    // Start is called before the first frame update
    void Start()
    {
        m_eIndex = ITEM.RESUME;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Use_Item()
    {
        //�׾��� ���� ����ǵ���
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Immediate_Resume();
    }
}
