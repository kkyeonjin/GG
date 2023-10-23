using Cinemachine;
using UnityEngine;

public class FlashlightArea : MonoBehaviour
{
    private CinemachineVirtualCamera closeCam;
    public static bool flashCamActivated = false;

    private void Awake()
    {
        if (closeCam = transform.Find("FlashCam").GetComponent<CinemachineVirtualCamera>())
        {
            Debug.Log("flash cam set");
        };
    }

    /*
    private void Update()
    {
        if (flashCamActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Click");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("click " + hit.collider.name);
                    if (hit.collider.name == "FlashlightArea")
                    {
                        hit.collider.gameObject.GetComponent<SubwayItems>().ItemPick();
                        Debug.Log("Pick flashlight");
                        hit.collider.gameObject.SetActive(false);

                        Phase1Mgr.clearCondition[1] = true;
                    }
                }
            }
        }
    }
    */


    private void OnTriggerStay(Collider other)
    {
        if (Phase1Mgr.Instance.earthquake.isQuake || Phase1Mgr.Instance.earthquake.isQuakeStop)
        {
            //��ȣ�ۿ� E
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //ī�޶� ��ȯ (PlayerCam -> closeCam)
                    closeCam.gameObject.SetActive(true);
                    flashCamActivated = true;
                    Debug.Log("flash cam activated");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ī�޶� ��ȯ (closeCam-> PlayerCam)
            closeCam.gameObject.SetActive(false);
            flashCamActivated = false;
            Debug.Log("flash cam deactivated");

        }
    }
}