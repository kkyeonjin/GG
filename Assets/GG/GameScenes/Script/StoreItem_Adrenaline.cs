using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Adrenaline : StoreItem
{
  
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
        m_fDuration = 1f;
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
                GameMgr.Instance.m_LocalPlayer.Adrenaline(false);
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
        GameMgr.Instance.m_LocalPlayer.Adrenaline(true);
        m_bActivate = true;
        m_fDurationTimer = 0f;
    }
    private void StartUpdate_Cooltime()
    {
        base.Use_Item();
    }
}
