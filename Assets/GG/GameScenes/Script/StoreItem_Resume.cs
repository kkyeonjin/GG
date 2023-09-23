using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Resume : StoreItem
{

    public StoreItem_Resume() : base()
    {
        m_eIndex = ITEM.RESUME;
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        m_fCoolTime = 10f;
        m_fTimer = 10f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void Use_Item()
    {
       
    }

    public void Consume_Item()//���� ���
    {
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Immediate_Resume();
    }
}
