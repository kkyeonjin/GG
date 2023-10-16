using UnityEngine;

public class SubwayItems : MonoBehaviour
{
    public Sprite icon; // 획득 시 인벤토리 창에 띄울 아이콘
    public Sprite ItemInfo;
    
    public int itemNum; // 아이템 고유 번호 부여(1부터)
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