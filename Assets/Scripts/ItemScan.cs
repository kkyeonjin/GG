using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    //public TMP_Text pressText;
    //Item 용도 설명 UI
    public Image ItemInfo;
    public GameObject pickedItem;

    //Press E UI
    public TMP_Text pressE;

    [SerializeField]
    float range;
    RaycastHit hitInfo;

    [SerializeField]
    LayerMask layerMask;

    private void Update()
    {
        CheckItem();
    }

    public void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag("Item"))
            {
                ItemInfo.sprite = hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemInfo;
                ItemInfo.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                    hitInfo.transform.gameObject.transform.SetParent(pickedItem.transform);
                    hitInfo.transform.gameObject.SetActive(false);
                }
            }
            else if (hitInfo.transform.gameObject.CompareTag("Elevator"))
            {
                pressE.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {

                }
            }
        }
        else
        {
            ItemInfo.gameObject.SetActive(false);
            pressE.gameObject.SetActive(false);
        }
    }
}