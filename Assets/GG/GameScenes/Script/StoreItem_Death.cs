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
    protected override void Start()
    {
        m_eIndex = ITEM.DEATH;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Use_Item()
    {
        base.Use_Item();
        GameMgr.Instance.BroadCast_Death();//���� �޴��� PV�� ��� �÷��̾�� ����
    }
}
