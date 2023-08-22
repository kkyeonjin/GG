using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem_Death : StoreItem
{

    public StoreItem_Death() : base()
    {
        m_eIndex = ITEM.DEATH;
        Debug.Log("Death!");
    }
    ~StoreItem_Death()
    {
        Debug.Log("�����!");
    }
    // Start is called before the first frame update
    void Start()
    {
        m_eIndex = ITEM.DEATH;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.BroadCast_Death();//���� �޴��� PV�� ��� �÷��̾�� ����
    }
}
