using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ITEM { RESUME, DEATH, ADRENALINE, POSTION, INVINCIBLE,END};

    public ITEM m_eIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Get_Index()
    {
        return (int)m_eIndex;
    }
    public void Set_Index(int iIndex)
    {
        m_eIndex = (ITEM)iIndex;
    }
}
