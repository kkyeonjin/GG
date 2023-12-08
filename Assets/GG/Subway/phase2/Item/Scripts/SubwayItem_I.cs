using UnityEngine;

public class SubwayItem_I : MonoBehaviour
{
    private bool isUsed = false;
    protected Player targetPlayer;
    public int itemNum; //4: KnockDown 5: SlowDown

    //public AudioClip itemGrabSound;
    //public AudioClip itemThrowSound;

    [Space(10)]
    public float damageRatio = 0.15f;


    private void Awake()
    {
        //this.itemType = SubwayItem.ItemType.INTERRUPT;
    }

    public void Item_effect()
    {
        //base.Item_effect(); //isUsed ÀüÈ¯
        //Item_grab();

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

}