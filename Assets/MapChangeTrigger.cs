using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChangeTrigger : MonoBehaviour
{
    public Camera minimapCam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MapChangePoint")) 
        {
            if(other.gameObject.GetComponent<MapChange>().floor == 4)
            {
                minimapCam.transform.localPosition = new Vector3(minimapCam.transform.localPosition.x, 10, minimapCam.transform.localPosition.z);
            }
            else if (other.gameObject.GetComponent<MapChange>().floor == 3)
            {
                minimapCam.transform.localPosition = new Vector3(minimapCam.transform.localPosition.x, 0, minimapCam.transform.localPosition.z);
            }
            else if (other.gameObject.GetComponent<MapChange>().floor == 2)
            {
                minimapCam.transform.localPosition = new Vector3(minimapCam.transform.localPosition.x, -10, minimapCam.transform.localPosition.z);
            }
            else if (other.gameObject.GetComponent<MapChange>().floor == 1)
            {
                minimapCam.transform.localPosition = new Vector3(minimapCam.transform.localPosition.x, -20, minimapCam.transform.localPosition.z);
            }
        }
    }
}
