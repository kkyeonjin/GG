using UnityEngine;

public class SubwayItems : MonoBehaviour
{
    public Sprite icon; // 획득 시 인벤토리 창에 띄울 아이콘
    public Sprite ItemInfo;
    public int itemNum; // 아이템 고유 번호 부여(1부터)
    public bool disposable;
    public int remainder;

    // Start is called before the first frame update

    public void ItemPick()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SubwayInventory.instance.invScripts[i] == null)
            {
                Debug.Log("SubwayItem Pick");
                SubwayInventory.instance.invScripts[i] = this.gameObject.GetComponent<SubwayItems>();
                SubwayInventory.instance.invIcons[i].sprite = icon;

                break;
            }
        }
    }

    public void ItemUse()
    {
        switch (itemNum)
        {
            case 0:
                break;
            default:
                break;
        }
    }

    public void ItemPause() //아이템 효과 해제(사용중지)
    {
        switch (itemNum)
        {
            case 0:
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    /*
    public enum ItemType
    {
        // 자기 강화
        HpPotion, //HP 회복 (Max의 30%p 증가)
        StaminaPotion, //스태미나 회복 (Max의 30%p 증가)
        FlashLight, //손전등

        // 상대 방해
        KnockDown, //넘어뜨리기 (Max HP의 15%p 감소)
        SlowDown, //달리기 속도 반감 5초
        AllSlowDown //(꼴등용) 전체 플레이어 달리기 속도 반감 5초
    }

    public ItemType itemType;
    public int effectValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemEffect(other.gameObject);
            Destroy(gameObject);
            
            //Player의 인벤토리에 추가 코드
        }
    }

    private void ItemEffect(GameObject player)
    {
        // itemType에 따라 다른 효과를 적용하는 코드 작성
        switch (itemType)
        {
            //자기 강화
            case ItemType.HpPotion:
                double recoverHp = player.GetComponent<CharacterStatus>().Get_MaxHP() * 0.3;
                //player의 status에 set_hp 함수 추가
                break;
            case ItemType.StaminaPotion:
                double recoverStamina = player.GetComponent<CharacterStatus>().Get_MaxStamina() * 0.3;
                //player의 status에 set_stamina 함수 추가
                break;
            case ItemType.FlashLight:
                break;

            //상대방해
            case ItemType.KnockDown:
                break;
            case ItemType.SlowDown:
                break;
            case ItemType AllSlowDown:
                break;

            default:

        }
    }
    */

}