using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InGameUIMgr : MonoBehaviour
{
    public static InGameUIMgr m_Instance;
    //�÷��̾� hp,stamina
    public StatusUI m_StatusUI;
    //�÷��̾� ������ ����
    public ItemSlotEffect[] m_StoreItemSlots;
    //�÷��̾� �׾��� �� �ߴ� UI��

    //���� ���� �Ŀ� �ߴ� Ui��

    void Awake()
    {

        var duplicated = FindObjectsOfType<InGameUIMgr>();

        if (duplicated.Length > 1)
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
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
