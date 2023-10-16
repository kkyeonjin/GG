using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using static System.Net.WebRequestMethods;

public class CharacterStatus : MonoBehaviour
{
    public Player m_Target;

    public float m_fSPRecover;
    public PhotonView m_PV;

    public float m_fMaxHP;
    public float m_fMaxStamina;

    private bool m_bIsUsable = true;
    private bool m_bDie = false;
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
        m_fMaxHP = m_fHP = fHP;
        m_fMaxStamina = m_fStamina = fStamina;
    }
  
    public void Resume_Immediate()
    {
        m_PV.RPC("Resume_Status", RpcTarget.All);
        m_bDie = false;
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
                m_bDie = true;
                m_Target.Player_Die();
                SingleGameMgr.Instance.Game_Over();
               //GameMgr 등에서 리스폰 창 불러오게
            }
        }
    }
    public bool is_Dead()
    {
        return m_bDie;
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

    public void Recover_Stamina_byItem(float fStamina)
    {
        if (m_PV != null)
            m_PV.RPC("Update_Stamina", RpcTarget.All, fStamina);
        else
        {
            m_fStamina += fStamina;
            if (m_fStamina > m_fMaxStamina)
            {
                m_fStamina = m_fMaxStamina;
            }
        }
    }

    public void PV_Reset()
    {
        m_PV.RPC("Reset_Status", RpcTarget.All);
        m_bDie = false;
    }

    [PunRPC]
    void Update_Damage(float fDamage)
    {
        m_fHP -= fDamage;
        if (0f >= m_fHP)
        {
            m_fHP = 0;
            m_bDie = true;
            m_Target.Player_Die();
        }
    }
    [PunRPC]
    void Update_HP(float fHP)
    {
        m_fHP += fHP;
        if (m_fHP > m_fMaxHP)
        {
            m_fHP = m_fMaxHP;
        }
    }
    [PunRPC]
    void Update_Stamina(float fStamina)
    {
        m_fStamina += fStamina;
        if (m_fStamina > m_fMaxStamina)
        {
            m_fStamina = m_fMaxStamina;
        }
    }
    [PunRPC]
    void Reset_Status()
    {
        m_fHP = m_fMaxHP;
        m_fStamina = m_fMaxStamina;
    }
    [PunRPC]
    void Resume_Status()
    {
        m_fHP = m_fMaxHP * 0.5f;
        m_fStamina = m_fMaxStamina;
    }
}
