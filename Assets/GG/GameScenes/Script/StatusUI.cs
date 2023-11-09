using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public CharacterStatus m_PlayerStatus;

    public UIEffect m_HPUI;
    public UIEffect m_StaminaUI;
    public UIEffect m_OrderUI;
    public bool subway = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerStatus != null)
        {
            int fMaxHP = (int)m_PlayerStatus.Get_MaxHP();
            int fHP = (int)m_PlayerStatus.Get_HP();
            float fRatio = (float)fHP / (float)fMaxHP;

            m_HPUI.Set_Ratio(fRatio);

            int fMaxStamina = (int)m_PlayerStatus.Get_MaxStamina();
            int fStamina = (int)m_PlayerStatus.Get_Stamina();
            fRatio = (float)fStamina / (float)fMaxStamina;

            m_StaminaUI.Set_Ratio(fRatio);

            if (subway == true)
            {
                int fMaxOrder = (int)SubwayInventory.instance.orderGage.Get_MaxOrder();
                int fOrder = (int)SubwayInventory.instance.orderGage.Get_Order();
                float fORatio = (float) fOrder / (float)fMaxOrder;

                Debug.Log("Order Ratio: " + fORatio);
                m_OrderUI.Set_Ratio(fORatio);
            }
        }
    }
}
