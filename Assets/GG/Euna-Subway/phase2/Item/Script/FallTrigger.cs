using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    //public GameObject[] FallObject;
    // Start is called before the first frame update


    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Free;
            this.GetComponent<BoxCollider>().enabled = false;
            /*
            //Debug.Log("Falling Trigger!");
            foreach(GameObject obj in FallObject)
            {
                //Destroy(obj.GetComponent<ConfigurableJoint>());
                //obj.GetComponent<ConfigurableJoint>().zMotion
                //= obj.GetComponent<ConfigurableJoint>().yMotion
                obj.GetComponent<ConfigurableJoint>().xMotion
                = ConfigurableJointMotion.Free;
            }
            */
        }
    }
}
