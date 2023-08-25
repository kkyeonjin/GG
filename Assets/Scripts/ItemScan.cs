using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    //public TMP_Text pressText;
    public Image ItemInfo;

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
                //Destroy(hitInfo.transform.gameObject);
            }
        }

        else
        {
            ItemInfo.gameObject.SetActive(false);
        }

    }

}