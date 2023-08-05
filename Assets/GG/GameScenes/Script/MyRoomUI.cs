using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomUI : MonoBehaviour
{
    public MyRoomMgr m_Manager;
    public void Change_Avatar(int iIndex)
    {
        m_Manager.Select_Avatar((Player.CHARACTER)iIndex);
    }

}
