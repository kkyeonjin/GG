using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomUI : MonoBehaviour
{
    public MyRoomMgr m_Manager;

    public int m_iCharacterIndex;
    public Button MyButton;
    public Image ButtonImage;

    private bool m_bIsSelected = false;


    private void Start()
    {
        if (m_iCharacterIndex > -1)
        {
            if (false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
            {
                MyButton.interactable = false;
                ButtonImage.color = new Color(0.8f, 0.8f, 0.8f);
            }
            else
            {
                Color color = MyButton.image.color;
                color.a = 0f;
                MyButton.image.color = color;
                if (InfoHandler.Instance.Get_CurrCharacter() == m_iCharacterIndex)
                {
                    m_Manager.Set_CurrAvatarUI(this);
                    m_bIsSelected = true;
                }
                Avatar_Selected(m_bIsSelected);
            }
        }
    }

    public bool Get_Selected()
    {
        return m_bIsSelected;
    }

    public int Get_CharacterIndex()
    {
        return m_iCharacterIndex;
    }

    public void Avatar_Selected(bool bInput)
    {
        m_bIsSelected = bInput;
        if(m_bIsSelected)
        {
            ButtonImage.color = new Color(1f, 1f, 1f);
        }
        else
        {
            ButtonImage.color = new Color(0.8f, 0.8f, 0.8f);
        }
    }

    public void Change_Avatar(int iIndex)
    {
        m_Manager.Select_Avatar(this);
    }

}
