using UnityEngine;

public class SubwayItem_E : SubwayItem
{
    public AudioClip itemEffectSound;

    [Space(10)]
    public float recoverRatio = 0.3f;


    protected override void Awake()
    {
        base.Awake();
        this.itemType = ItemType.ENFORCEMENT;
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
                //SubwayInventory.instance.orderGage.Recover_Order(recoverRatio);
                GameMgr.Instance.m_LocalPlayer.OrderPotion(recoverRatio);
                break;
            default:
                return;
        }

        itemAudioSrc.clip = itemEffectSound;
        itemAudioSrc.Play();

        Destroy(this.gameObject);
    }
}