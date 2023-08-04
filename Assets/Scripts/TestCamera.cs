using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestCamera : MonoBehaviour
{
    public TMP_Text pressText;
    float mouseSpeed = 10;
    float mouseY = 0;
    float mouseX = 0;

    [SerializeField]
    float range;
    RaycastHit hitInfo;

    [SerializeField]
    LayerMask layerMask;

    private void Update()
    {
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed;
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed;

        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);
        mouseX = Mathf.Clamp(mouseX, -55.0f, 55.0f);

        transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        checkItem();
    }

    public void checkItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
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