using UnityEngine;

public class GameItem : MonoBehaviour
{
    public enum ItemType
    {
        // �ڱ� ��ȭ
        HpPotion, //HP ȸ�� (Max�� 30%p ����)
        StaminaPotion, //���¹̳� ȸ�� (Max�� 30%p ����)
        FlashLight, //������

        // ��� ����
        KnockDown, //�Ѿ�߸��� (Max HP�� 15%p ����)
        SlowDown, //�޸��� �ӵ� �ݰ� 5��
        AllSlowDown //(�õ��) ��ü �÷��̾� �޸��� �ӵ� �ݰ� 5��
    }

    public ItemType itemType;
    public int effectValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemEffect(other.gameObject);
            Destroy(gameObject);
            
            //Player�� �κ��丮�� �߰� �ڵ�
        }
    }

    private void ItemEffect(GameObject player)
    {
        // itemType�� ���� �ٸ� ȿ���� �����ϴ� �ڵ� �ۼ�
        switch (itemType)
        {
            //�ڱ� ��ȭ
            case ItemType.HpPotion:
                double recoverHp = player.GetComponent<CharacterStatus>().Get_MaxHP() * 0.3;
                //player�� status�� set_hp �Լ� �߰�
                break;
            case ItemType.StaminaPotion:
                double recoverStamina = player.GetComponent<CharacterStatus>().Get_MaxStamina() * 0.3;
                //player�� status�� set_stamina �Լ� �߰�
                break;
            case ItemType.FlashLight:
                break;

            //������
            case ItemType.KnockDown:
                break;
            case ItemType.SlowDown:
                break;
            case ItemType AllSlowDown:
                break;

            default:

        }
    }


}