using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Resume : StoreItem
{

    public StoreItem_Resume() : base()
    {
        m_eIndex = ITEM.RESUME;
        Debug.Log("Resume!");

    }
    ~StoreItem_Resume()
    {
        Debug.Log("사라짐!");
    }
    // Start is called before the first frame update
    protected override void Start()
    {
    
        m_eIndex = ITEM.RESUME;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void Use_Item()
    {
        //죽었을 때만 실행되도록
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Immediate_Resume();
    }
}
