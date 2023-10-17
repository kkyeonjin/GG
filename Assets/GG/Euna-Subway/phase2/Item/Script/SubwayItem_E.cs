using UnityEngine;

public class SubwayItem_E : SubwayItem
{
    public float recoverRatio = 0.3f;

    private void Awake()
    {
        this.itemType = SubwayItem.ItemType.ENFORCEMENT;
    }

    public override void Item_effect()
    {
        base.Item_effect();

        switch (this.itemNum)
        {
            case 1: //Hp Potion
                GameMgr.Instance.m_LocalPlayer.HpPotion(recoverRatio);
                break;
            case 2: //Stamina Potion
                GameMgr.Instance.m_LocalPlayer.StaminaPotion(recoverRatio);
                break;
            case 3: //OrderGage Potion
                //OrderGage 및 회복함수 player에 추가
                break;
            default:
                return;
        }

        Destroy(this.gameObject);
    }
}