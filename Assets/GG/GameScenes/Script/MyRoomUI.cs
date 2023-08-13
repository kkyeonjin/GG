using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyRoomUI : MonoBehaviour
{
    public MyRoomMgr m_Manager;

    public int m_iCharacterIndex;
    public Button MyButton;

    AvatarStatus AvatarStatus;

    private void Start()
    {
        if (m_iCharacterIndex > -1 && false == InfoHandler.Instance.Is_Character_Available(m_iCharacterIndex))
            MyButton.interactable = false;
    }

    public void Change_Avatar(int iIndex)
    {
        m_Manager.Select_Avatar((Player.CHARACTER)iIndex);
    }

}
