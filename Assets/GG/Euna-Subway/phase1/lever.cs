using Cinemachine;
using UnityEngine;


public class lever : MonoBehaviour
{
    private int clickCount = 0;
    private const float rotOffset = 3;
    private const float clearCount = 90 / rotOffset;

    private void Update()
    {
        if (EmergencyLever.leverCamActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                float leverXRot = transform.localEulerAngles.x;

                if (Physics.Raycast(ray, out hit))
                {
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.name == "LeverPoint")
                    {
                        clickCount++;
                        leverXRot -= rotOffset;
                        transform.localEulerAngles = new Vector3(leverXRot, 0f, 0f);
                        Debug.Log(transform.localEulerAngles);

                        if (clickCount >= clearCount)
                        {
                            Phase1Manager.clearCondition[2] = true;
                            Debug.Log(Phase1Manager.clearCondition[2]);
                            //closeCam.gameObject.SetActive(false);
                        }
                    }

                }
            }
        }
        
    }
}