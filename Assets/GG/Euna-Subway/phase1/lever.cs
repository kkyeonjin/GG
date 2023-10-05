using Cinemachine;
using UnityEngine;


public class Lever : MonoBehaviour
{
    private int clickCount = 0;
    public const float rotOffset = 3;
    private const float clearCount = 90 / rotOffset;

    private float leverXRot;

    private void Start()
    {
        leverXRot = transform.localEulerAngles.x;   
    }

    public void turn_lever()
    {
        leverXRot -= rotOffset;
        transform.localEulerAngles = new Vector3(leverXRot, 0f, 0f);
        Debug.Log(transform.localEulerAngles);
    }

    public void add_clickCount()
    {
        clickCount++;
    }

    public void check_ifClear()
    {
        if (clickCount >= clearCount)
        {
            Phase1Manager.clearCondition[2] = true;
            Debug.Log(Phase1Manager.clearCondition[2]);
        }
    }
           
    /*
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
                        //clickCount++;
                        add_clickCount();

                        //leverXRot -= rotOffset;
                        //transform.localEulerAngles = new Vector3(leverXRot, 0f, 0f);
                        //Debug.Log(transform.localEulerAngles);
                        turn_lever();

                        
                        //if (clickCount >= clearCount)
                        //{
                        //    Phase1Manager.clearCondition[2] = true;
                        //    Debug.Log(Phase1Manager.clearCondition[2]);
                        //    closeCam.gameObject.SetActive(false);
                        //}
                        
                        check_ifClear();
                    }

                }
            }
        }
        
    }
    */
}