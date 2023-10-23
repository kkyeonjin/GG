using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrigger : MonoBehaviour
{
    //public GameObject[] FallObject;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.gameObject.tag = "Obstacle";
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //진도 4 이상일 때만 물체 추락
        if((collider.gameObject.CompareTag("AI") || collider.gameObject.CompareTag("Player")))
        {
            this.gameObject.GetComponent<ConfigurableJoint>().xMotion = ConfigurableJointMotion.Free;

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
