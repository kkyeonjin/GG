using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Invincible : StoreItem
{
    private float m_fDuration;
    private float m_fDurationTimer;
    // Start is called before the first frame update
    void Start()
    {
        m_eIndex = ITEM.INVINCIBLE;
    }

    // Update is called once per frame
    void Update()
    {
        m_fDurationTimer += Time.deltaTime;
        if (m_fDurationTimer >= m_fDuration)
            GameMgr.Instance.m_LocalPlayer.Invincible(false);
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.m_LocalPlayer.Invincible(true);
    }
}
