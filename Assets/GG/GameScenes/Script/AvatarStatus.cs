using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarStatus : MonoBehaviour
{
    public float m_fHP;
    public float m_fStamina;
    public float m_fSpeed;

    public float Get_HP()
    {
        return m_fHP;
    }
    public float Get_Stamina()
    {
        return m_fStamina;
    }
    public float Get_Speed()
    {
        return m_fSpeed;
    }
}