using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public CharacterStatus m_PlayerStatus;

    public ItemSlotEffect m_HPUI;
    public ItemSlotEffect m_StaminaUI;

    void Start()
    {
        m_PlayerStatus = GameMgr.Instance.m_LocalPlayer.GetComponentInChildren<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {//��� �θ�. ��ȭ�� �Ͼ�� ���� ȣ���ϵ���
        int fMaxHP = (int)m_PlayerStatus.Get_MaxHP();
        int fHP = (int)m_PlayerStatus.Get_HP();
        float fRatio = (float)fHP / (float)fMaxHP;

        m_HPUI.Set_Ratio(fRatio);

        int fMaxStamina = (int)m_PlayerStatus.Get_MaxStamina();
        int fStamina = (int)m_PlayerStatus.Get_Stamina();
        fRatio = (float)fStamina / (float)fMaxStamina;

        m_StaminaUI.Set_Ratio(fRatio);
        
    }
}
