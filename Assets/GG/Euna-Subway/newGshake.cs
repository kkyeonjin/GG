using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGshake : MonoBehaviour
{
    public static bool isShake = false;
    //How strong is the earthquake?
    public float magnitude; //Not the same magnitude people talk about in an actual earthquakes
    public float slowDownFactor = 0.1f;

    private Vector3 originalPosition;

    Vector2 randomPos;

    Vector3 moveVecR;
    float randomY;
    float randomX;
    float randomZ;

    public static Vector3 moveVecR_q;

    void Start()
    {
        originalPosition = transform.localPosition;
        magnitude = Random.Range(1, 8);
        Debug.Log(magnitude);

        isShake = true;
    }

    void FixedUpdate()
    {
        //Debug.Log(transform.localPosition);
        randomPos = Random.insideUnitCircle * magnitude * 50;

        randomY = Random.Range(-1f, 1f) * magnitude * 50;

        randomX = Mathf.Lerp(transform.localPosition.x, randomPos.x, Time.deltaTime * slowDownFactor);
        randomZ = Mathf.Lerp(transform.localPosition.z, randomPos.x, Time.deltaTime * slowDownFactor);

        randomY = Mathf.Lerp(transform.localPosition.y, randomY, Time.deltaTime * slowDownFactor * 0.1f);

        //Vector3 moveVec = new Vector3(randomX * 0.6f, randomY * 0.6f, randomZ * 0.6f);
        moveVecR = new Vector3(randomX * 1.2f, randomY * 1.2f, randomZ * 1.2f);

        //transform.localPosition = originalPosition + moveVec;
        transform.rotation = Quaternion.Euler(moveVecR);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 movePVecR = new Vector3(randomX * 1.8f, randomY * 1.8f, randomZ * 1.8f);

            collision.gameObject.transform.rotation = Quaternion.Euler(movePVecR);
            //collision.gameObject.transform.Find("Camera").transform.rotation = Quaternion.Euler(movePVecR);
            Debug.Log("Player On EQ");
        }
        */

        
        if (collision.rigidbody != null)
        {
            moveVecR_q = moveVecR * 50;
            //Debug.Log(moveVecR_q);
            //Debug.Log(collision.gameObject.name);
            collision.rigidbody.AddForce(moveVecR_q);
        }
        

        /*
        if (collision.collider.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            //Debug.Log("Player Found");
            GameObject subCam = player.transform.Find("Cams").Find("SubCamera").gameObject;


        }
        */

    }
}