using UnityEngine;



[System.Serializable]
public class SubwayItem
{
    public enum ItemType
    {
        // �ڱ� ��ȭ
        HPCHARGE,
        STAMINA, //���¹̳� ȸ�� (Max�� 30%p ����)

        // ��� ����
        KNOCKDOWN, //�Ѿ�߸��� (Max HP�� 15%p ����)
        SLOWDOWN, //�޸��� �ӵ� �ݰ� 5��
        SLOWDOWNALL //(�õ��) ��ü �÷��̾� �޸��� �ӵ� �ݰ� 5��
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false; //������ ��� ���� ����
    }
}