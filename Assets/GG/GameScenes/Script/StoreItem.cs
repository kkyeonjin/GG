using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public enum ITEM { RESUME, DEATH, ADRENALINE, POSTION, INVINCIBLE, END };

    private int m_iNum;
    protected ITEM m_eIndex = ITEM.END;

    protected float m_fCoolTime;
    protected float m_fTimer;

    protected bool m_bUsable;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public float Get_CoolTime_Ratio()//ui 쿨타임 표시용
    {
        return m_fTimer / m_fCoolTime;
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

    public virtual void Use_Item()
    {
        m_bUsable = false;
        m_fTimer = 0f;
    }
    
    
}
