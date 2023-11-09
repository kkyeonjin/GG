using UnityEngine;

public class SubwayItem_I : SubwayItem
{
    public float damageRatio = 0.15f;
    protected Player targetPlayer;


    private void Awake()
    {
        this.itemType = SubwayItem.ItemType.INTERRUPT;
    }

    public override void Item_effect()
    {
        base.Item_effect(); //isUsed 전환
        Item_grab();
        if (targetPlayer != null)
        {
            switch (this.itemNum)
            {
                case 4: //KnockDown
                    targetPlayer.KnockDown(damageRatio);
                    break;
                case 5: //SlowDown
                    targetPlayer.SlowDown(targetPlayer.m_fSpeed / 2);
                    break;
                case 6: //StoreItem - death
                    break;
                default:
                    return;
            }
        }
    }

    public void Item_grab() //투척해야 하는 방해형 아이템 클래스만 상속하는 함수
    {
        //아이템에 따라 Item Manager에서 받아올 아이템 프리팹 인덱스 조정
        int idx = 0;
        switch (this.itemNum)
        {
            case 4: //KnockDown
                idx = 0;
                break;
            case 5: //SlowDown
                idx = 1;
                break;
            case 7: //Death(상점 아이템)
                idx = 2;
                break;
            default:
                Debug.Log("grab return");
                return;
        }

        //player 손에 아이템 오브젝트 소환
        GameObject onHandPos = GameMgr.Instance.m_LocalPlayer.GetComponent<Player>().OnHand;
        GameObject grabbedItem = Instantiate(SubwayItemMgr.Instance.GrabbableItems[idx], onHandPos.transform.position, Quaternion.identity);
        grabbedItem.transform.SetParent(onHandPos.transform);

        //조준 및 투척 준비
        GameMgr.Instance.m_LocalPlayer.m_bIsThrow = true;

        SubwayInventory.instance.Active_AimPoint(true);

        Debug.Log("grab "+this.gameObject.name);
    }
}