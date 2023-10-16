using UnityEngine;

public class SubwayItem_EHpPotion : SubwayItem
{
    public const float healRatio = 0.3f;

    public SubwayItem_EHpPotion() : base() 
    {
        itemType = ItemType.ENFORCEMENT;
        Debug.Log("HP potion");
    }

    ~SubwayItem_EHpPotion()
    {
        Debug.Log("~HP potion");
    }

    public override void Use_Item()
    {
        GameMgr.Instance.m_LocalPlayer.HpPotion(healRatio);

        //CharacterStatus.cs¿¡ Set_HP Ãß°¡
        //GameMgr.Instance.m_LocalPlayer.GetComponent<CharacterStatus>().hp
    }
}