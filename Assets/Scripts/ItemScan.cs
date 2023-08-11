using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScan : MonoBehaviour
{
    public TMP_Text pressText;

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

            //+++


            hitInfo.transform.gameObject.GetComponent<PickableItem>().OutlineOn();

            if (hitInfo.transform.tag == "cup")
            {
                Debug.Log("cup detected");
                pressText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                    Destroy(hitInfo.transform.gameObject);
                }
            }

            else if (hitInfo.transform.tag == "tv")
            {
                Debug.Log("tv detected");
                pressText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                    Destroy(hitInfo.transform.gameObject);
                }
            }

            else if (hitInfo.transform.tag == "plant")
            {
                Debug.Log("plant detected");
                pressText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hitInfo.transform.gameObject.GetComponent<PickableItem>().ItemPick();
                    Destroy(hitInfo.transform.gameObject);
                }
            }

        }
        else
        {
            pressText.gameObject.SetActive(false);
        }

    }

}