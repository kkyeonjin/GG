using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Adrenaline : StoreItem
{
    private float m_fDuration;
    private float m_fDurationTimer;
    // Start is called before the first frame update
    public StoreItem_Adrenaline() : base()
    {
        m_eIndex = ITEM.ADRENALINE;
        Debug.Log("Adrenaline!");

    }
    ~StoreItem_Adrenaline()
    {
        Debug.Log("»ç¶óÁü!");
    }
    protected override void Start()
    {
        m_eIndex = ITEM.ADRENALINE;
    }

    // Update is called once per frame
    void Update()
    {
        m_fDurationTimer += Time.deltaTime;
        if (m_fDurationTimer >= m_fDuration)
            GameMgr.Instance.m_LocalPlayer.Adrenaline(false);
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Adrenaline(true);
    }
}
