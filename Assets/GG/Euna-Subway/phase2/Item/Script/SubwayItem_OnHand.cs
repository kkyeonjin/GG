using System.Collections.Generic;
using UnityEngine;

public class SubwayItem_OnHand : MonoBehaviour
{
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);
    public RectTransform OnHand_Position;

    private GameObject grabbedItem;

    private void Awake()
    {
        OnHand_Position = GetComponent<RectTransform>();
    }

    public void Item_Grab(int itemNum)
    {
        int i = 0;
        switch (itemNum)
        {
            case 4: //KnockDown
                i = 0;
                break;
            case 5: //SlowDown
                i = 1;
                break;
            case 7: //Death(상점 아이템)
                i = 2;
                break;

            default:
                break;
        }

        grabbedItem = Instantiate(GrabbableItems[i], OnHand_Position.position, Quaternion.identity);
        grabbedItem.transform.localScale = OnHand_Position.localScale;
    }

    public GameObject Get_grabbedItem()
    {
        return grabbedItem;
    }
}