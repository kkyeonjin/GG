using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Invincible : StoreItem
{

    public StoreItem_Invincible() : base()
    {
        m_eIndex = ITEM.INVINCIBLE;
        Debug.Log("Invincible!");

    }
    ~StoreItem_Invincible()
    {
        Debug.Log("»ç¶óÁü!");
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        m_eIndex = ITEM.INVINCIBLE;
        m_fDuration = 5f;
        m_fDurationTimer = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_bActivate)
        {
            m_fDurationTimer += Time.deltaTime;

            if (m_fDurationTimer >= m_fDuration)
            {
                GameMgr.Instance.m_LocalPlayer.Invincible(false);
                m_bActivate = false;
                StartUpdate_Cooltime();
            } 
        }
        else 
        { 
            base.Update();
        }
    }

    public override void Use_Item()
    {
        GameMgr.Instance.m_LocalPlayer.Invincible(true);
        m_bActivate = true;
        m_fDurationTimer = 0f;
        m_Effect.Activate_Item();

    }
    private void StartUpdate_Cooltime()
    {
        base.Use_Item();
    }
}
