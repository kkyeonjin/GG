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
        Debug.Log("사라짐!");
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        m_eIndex = ITEM.DEATH;
        m_bOnOff = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(m_bActivate)
        {
            Vector3 vOrigin = Camera.main.transform.position;
            Vector3 vDir = Camera.main.transform.forward;
            RaycastHit Info;
            int layer = 1 << LayerMask.NameToLayer("OtherPlayer");

            if (Physics.Raycast(vOrigin, vDir, out Info, 10f, layer))
            {
                //가운데 타게팅 하이라이팅 되는 기능 추가
                if (Input.GetMouseButtonDown(0))
                {
                    Info.collider.gameObject.GetComponent<Player>().Immediate_Death();
                    Utilized();
                }
            }
            
 
        }
    }

    public override void Use_Item()
    {
        m_bActivate = !m_bActivate;
        //GameMgr.Instance.Casting_Death();
        m_Effect.Activate_Item(m_bActivate);
    }

    private void Utilized()
    {
        base.Use_Item();
    }
}
