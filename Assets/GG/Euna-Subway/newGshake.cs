using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGshake : MonoBehaviour
{
    //How strong is the earthquake?
    public float magnitude; //Not the same magnitude people talk about in an actual earthquakes
    public float slowDownFactor = 0.1f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
        magnitude = Random.Range(1, 8);
        Debug.Log(magnitude);
    }

    void FixedUpdate()
    {
        //Debug.Log(transform.localPosition);
        Vector2 randomPos = Random.insideUnitCircle * magnitude * 40;

        float randomY = Random.Range(-1f, 1f) * magnitude * 40;

        float randomX = Mathf.Lerp(transform.localPosition.x, randomPos.x, Time.deltaTime * slowDownFactor);
        float randomZ = Mathf.Lerp(transform.localPosition.z, randomPos.x, Time.deltaTime * slowDownFactor);

        randomY = Mathf.Lerp(transform.localPosition.y, randomY, Time.deltaTime * slowDownFactor * 0.1f);

        //Vector3 moveVec = new Vector3(randomX * 0.6f, randomY * 0.6f, randomZ * 0.6f);
        Vector3 moveVecR = new Vector3(randomX * 1.2f, randomY * 1.2f, randomZ * 1.2f);

        //transform.localPosition = originalPosition + moveVec;
        transform.rotation = Quaternion.Euler(moveVecR);
   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 randomForce = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * magnitude;
            collision.rigidbody.AddForce(randomForce, ForceMode.Impulse);
        }

    }
}