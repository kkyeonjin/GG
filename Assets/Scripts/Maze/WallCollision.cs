using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void Awake()
    {
            Collider[] colliders = Physics.OverlapSphere(transform.position, .01f);

            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Wall")
                {
                //Debug.Log("walldelete");
              collider.gameObject.SetActive(false);
                    //Destroy(collider.gameObject);
                    return;
                }
            }

            this.GetComponent<Collider>().enabled = true;   
      //    Debug.Log("walldelete");
        
    }
    
}
