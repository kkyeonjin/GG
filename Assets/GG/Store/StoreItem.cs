using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public enum ITEM { RESUME, DEATH, ADRENALINE, POTION, INVINCIBLE, END };

    private int m_iNum;
    protected ITEM m_eIndex;

    protected float m_fTimer=1f;

    protected bool m_bUsable = true;

    protected float m_fDuration;
    protected float m_fDurationTimer;

    protected bool m_bActivate = false;
    protected bool m_bOnOff = false;

    protected ItemSlotEffect m_Effect;

    public StoreItem()
    {
        Debug.Log("storeItem »ý¼º!");
        m_eIndex = ITEM.END;
    }

    protected virtual void Start()
    {
       
    }
    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public bool Get_Activated()
    {
        return m_bActivate;
    }

    public void Set_Num(int iNum)
    {
        m_iNum = iNum;
    }
    public int Get_Num()
    {
        return m_iNum;
    }

    public bool Is_Usable()
    {
        return m_bUsable;
    }

    public int Get_ItemIndex()
    {
        return (int)m_eIndex;
    }

    public void Set_EffectScript(ItemSlotEffect Input)
    {
        m_Effect = Input;
    }

    public virtual void Use_Item()
    {
        if (m_eIndex == ITEM.END)
            return;

        m_bUsable = false;
        m_Effect.Use_Item();
    }

    public virtual void On_Item()
    {

    }
    public virtual void Off_Item()
    {

    }
    
    
}
