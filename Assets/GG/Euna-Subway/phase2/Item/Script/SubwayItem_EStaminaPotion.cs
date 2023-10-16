using UnityEngine;

public class SubwayItem_EStaminaPotion : SubwayItem
{
    public const float healRatio = 0.3f;

    public SubwayItem_EStaminaPotion() : base() 
    {
        itemType = ItemType.ENFORCEMENT;
        Debug.Log("Stamina potion");
    }

    ~SubwayItem_EStaminaPotion()
    {
        Debug.Log("~Stamina potion");
    }

    public override void Use_Item()
    {
        GameMgr.Instance.m_LocalPlayer.StaminaPotion(healRatio);

        //CharacterStatus.cs¿¡ Set_HP Ãß°¡
        //GameMgr.Instance.m_LocalPlayer.GetComponent<CharacterStatus>().hp
    }
}