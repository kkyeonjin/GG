using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    //public TMP_Text pressText;
    public Image ItemInfo;
    public GameObject pickedItem;

    [SerializeField]
    float range;
    RaycastHit hitInfo;

    [SerializeField]
    LayerMask layerMask;

    private void Update()
    {
        checkItem();
    }

    public void checkItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {

            ItemInfo.sprite = hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemInfo;
            ItemInfo.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                hitInfo.transform.gameObject.transform.SetParent(pickedItem.transform);
                hitInfo.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
                hitInfo.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
            }
        }

        else
        {
            ItemInfo.gameObject.SetActive(false);
        }

    }

}