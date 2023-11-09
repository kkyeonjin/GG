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
        base.Item_effect(); //isUsed ��ȯ
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

    public void Item_grab() //��ô�ؾ� �ϴ� ������ ������ Ŭ������ ����ϴ� �Լ�
    {
        //�����ۿ� ���� Item Manager���� �޾ƿ� ������ ������ �ε��� ����
        int idx = 0;
        switch (this.itemNum)
        {
            case 4: //KnockDown
                idx = 0;
                break;
            case 5: //SlowDown
                idx = 1;
                break;
            case 7: //Death(���� ������)
                idx = 2;
                break;
            default:
                Debug.Log("grab return");
                return;
        }

        //player �տ� ������ ������Ʈ ��ȯ
        GameObject onHandPos = GameMgr.Instance.m_LocalPlayer.GetComponent<Player>().OnHand;
        GameObject grabbedItem = Instantiate(SubwayItemMgr.Instance.GrabbableItems[idx], onHandPos.transform.position, Quaternion.identity);
        grabbedItem.transform.SetParent(onHandPos.transform);

        //���� �� ��ô �غ�
        GameMgr.Instance.m_LocalPlayer.m_bIsThrow = true;

        SubwayInventory.instance.Active_AimPoint(true);

        Debug.Log("grab "+this.gameObject.name);
    }
}