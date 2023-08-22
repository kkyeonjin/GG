using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : MonoBehaviour
{
    public enum ITEM { RESUME, DEATH, ADRENALINE, POSTION, INVINCIBLE, END };

    private int m_iNum;
    protected ITEM m_eIndex;

    protected float m_fCoolTime=1f;
    protected float m_fTimer=1f;

    protected bool m_bUsable;

    public StoreItem()
    {
        Debug.Log("storeItem 생성!");
        m_eIndex = ITEM.END;
    }

    protected virtual void Start()
    {
       
    }
    // Update is called once per frame
    protected virtual void Update()
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
        Debug.Log("호출! " + m_eIndex);
        m_bUsable = false;
        m_fTimer = 0f;
    }
    
    
}
