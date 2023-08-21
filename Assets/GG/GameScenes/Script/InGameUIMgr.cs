using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InGameUIMgr : MonoBehaviour
{
    public static InGameUIMgr m_Instance;
    //플레이어 hp,stamina
    public StatusUI m_StatusUI;
    //플레이어 아이템 슬롯
    public ItemSlotEffect[] m_StoreItemSlots;
    //플레이어 죽었을 때 뜨는 UI들

    //게임 끝난 후에 뜨는 Ui들

    void Awake()
    {

        var duplicated = FindObjectsOfType<InGameUIMgr>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }


    }
    public static InGameUIMgr Instance
    {
        get
        {
            if (null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set_Item(int iIndex, StoreItem iInput)
    {
        m_StoreItemSlots[iIndex].Set_Item(iInput);
    }
    
}
