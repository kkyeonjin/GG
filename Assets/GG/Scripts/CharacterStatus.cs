using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CharacterStatus : MonoBehaviour
{
    public Player m_Target;
    public EventUI m_EventUI;
    public float m_fSPRecover;
    public PhotonView m_PV;

    public float m_fMaxHP;
    public float m_fMaxStamina;

    private bool m_bIsUsable = true;
    private float m_fHP;
    private float m_fStamina;

    void Start()
    {
        m_fHP = m_fMaxHP;
        m_fStamina = m_fMaxStamina;
    }

    private void Update()
    {
        Recover_Stamina();
    }

    public void Change_Status(float fHP, float fStamina)
    {
        m_fMaxHP = fHP;
        m_fMaxStamina = fStamina;
    }

    public float Get_HP()
    {
        return m_fHP;
    }
    public float Get_Stamina()
    {
        return m_fStamina;
    }

    public float Get_MaxHP()
    {
        return m_fMaxHP;
    }
    public float Get_MaxStamina()
    {
        return m_fMaxStamina;
    }

    public void Set_Damage(float fDamage)
    {
        if (m_PV != null)
            m_PV.RPC("Update_Damage", RpcTarget.All, fDamage);
        else
        {
            m_fHP -= fDamage;
            if (0f >= m_fHP)
            {
                m_fHP = 0;
                m_Target.Set_Dead();
                m_EventUI.Activate_and_Over();
            }
        }
    }
    public bool Is_Usable()
    {
        return m_bIsUsable;
    }
    public void Use_Stamina(float fStamina)
    {
        m_fStamina -= fStamina*Time.deltaTime;
        if(0f> m_fStamina)
        {
            m_fStamina = 0;
            m_bIsUsable = false;
        }
    }

    public void Recover_HP(float fHP)
    {
        if (m_PV != null)
            m_PV.RPC("Update_HP", RpcTarget.All, fHP);
        else
        {
            m_fHP += fHP;
            if (m_fHP > m_fMaxHP)
            {
                m_fHP = m_fMaxHP;
            }
        }
    }
    private void Recover_Stamina()
    {
        m_fStamina += m_fSPRecover*Time.deltaTime;
        if (m_fStamina > 10f)
            m_bIsUsable = true;

        if (m_fStamina > m_fMaxStamina)
            m_fStamina = m_fMaxStamina;
    }

    [PunRPC]
    void Update_Damage(float fDamage)
    {
        m_fHP -= fDamage;
        if (0f >= m_fHP)
        {
            m_fHP = 0;
            m_Target.Set_Dead();
            m_EventUI.Activate_and_Over();
        }
    }
    void Update_HP(float fHP)
    {
        m_fHP += fHP;
        if (m_fHP > m_fMaxHP)
        {
            m_fHP = m_fMaxHP;
        }
    }
}
