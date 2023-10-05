using Cinemachine;
using UnityEngine;

public class Flashlight : MonoBehaviour
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
                    if (hit.collider.name == "Flashlight")
                    {
                        hit.collider.gameObject.GetComponent<SubwayItems>().ItemPick();
                        Debug.Log("Pick flashlight");
                        hit.collider.gameObject.SetActive(false);

                        Phase1Manager.clearCondition[1] = true;
                    }

                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Earthquake.isQuake || Earthquake.isQuakeStop)
        {
            //상호작용 E
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //카메라 전환 (PlayerCam -> closeCam)
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
            //카메라 전환 (closeCam-> PlayerCam)
            closeCam.gameObject.SetActive(false);
            flashCamActivated = false;
            Debug.Log("flash cam deactivated");

        }
    }
}