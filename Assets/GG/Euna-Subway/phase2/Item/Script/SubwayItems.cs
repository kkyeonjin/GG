using UnityEngine;

public class SubwayItems : MonoBehaviour
{
    public Sprite icon; // ȹ�� �� �κ��丮 â�� ��� ������
    public Sprite ItemInfo;
    
    public int itemNum; // ������ ���� ��ȣ �ο�(1����)
    public bool used;


    public bool ItemPick()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SubwayInventory.instance.invScripts[i] == null)
            {
                Debug.Log("SubwayItem Pick");
                //SubwayInventory.instance.invScripts[i] = this.gameObject.GetComponent<SubwayItems>();
                SubwayInventory.instance.invIcons[i].sprite = icon;
                return true;
            }
        }
        return false;
    }

}