using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Earthquake : MonoBehaviour
{
    public float magnitude;
    public List<Rigidbody> DropObjects;
    public List<Light> ControlLights;
    private Vector3 originalPosition;

    Vector2 randomPos;

    Vector3 moveVecR;
    float randomY;
    float randomX;
    float randomZ;

    public static Vector3 moveVecR_q;
    
    float Timer = 0f;
    float LitIntensity1=0;
    float LitIntensity2=0;

    //public Transform t1;
    //public Transform t2;

    private void Awake()
    {
        //bodies = FindObjectsOfType<Rigidbody>(); // 모든 Rigidbody 찾기
    }

    private void Start()
    {
        originalPosition = transform.localPosition;
        //magnitude = Random.Range(0.1f, 0.8f);

        //phase2 테스트용 추후 isQuake static 수정
        //isQuake = true;
        Debug.Log(magnitude);
    }

    void FixedUpdate()
    {
        
       quake();
       
    }

    public void quake()
    {
        //Debug.Log(transform.localPosition);
        //randomPos = Random.insideUnitCircle * magnitude * 50;

        //randomY = Random.Range(-1f, 1f) * magnitude * 50;

        //randomX = Mathf.Lerp(transform.localPosition.x, randomPos.x, Time.deltaTime);
        //randomZ = Mathf.Lerp(transform.localPosition.z, randomPos.x, Time.deltaTime);

        //randomY = Mathf.Lerp(transform.localPosition.y, randomY, Time.deltaTime);

        //Vector3 moveVec = new Vector3(randomX * 0.6f, randomY * 0.6f, randomZ * 0.6f);
        //moveVecR = new Vector3(randomX * 1.2f, randomY * 1.2f, randomZ * 1.2f);

        //transform.localPosition = originalPosition + moveVec;
        /*
        foreach(Rigidbody body in bodies)
        {
            body.transform.rotation = Quaternion.Euler(moveVecR);
        }
        */
        //transform.rotation = Quaternion.Euler(moveVecR);

        Vector2 vrandomCircleUnit = Random.insideUnitCircle * magnitude;
        Vector3 vRandomDir = new Vector3(vrandomCircleUnit.x, 0f, vrandomCircleUnit.y);

        transform.position = originalPosition + vRandomDir;

        randomPos = Random.insideUnitCircle * magnitude;

        randomY = Random.Range(-1f, 1f) * magnitude;

        randomX = Mathf.Lerp(transform.localPosition.x, randomPos.x, Time.deltaTime);
        randomZ = Mathf.Lerp(transform.localPosition.z, randomPos.x, Time.deltaTime);

        randomY = Mathf.Lerp(transform.localPosition.y, randomY, Time.deltaTime* 0.1f);

        //Vector3 moveVec = new Vector3(randomX * 0.6f, randomY * 0.6f, randomZ * 0.6f);
        moveVecR = new Vector3(randomX * 1.2f, randomY * 1.2f, randomZ * 1.2f);



        //transform.localPosition = originalPosition + moveVec;
        /*
        foreach(Rigidbody body in bodies)
        {
            body.transform.rotation = Quaternion.Euler(moveVecR);
        }
        */
        transform.rotation = Quaternion.Euler(moveVecR);

    }

    private void Update()
    {
        Control_Objects();

    }

    void Control_Objects()
    {
        Timer += Time.deltaTime;
        if (Timer >= 0.1f)
        {
            int random = Random.Range(1, 9);
            LitIntensity1 = random % 3 == 0 ? 0f : 2f;
            //random = Random.Range(1, 9);
            //LitIntensity2 = random % 3 == 0 ? 0f : 2f;

            Timer = 0f;

            int value = Random.Range(0, 75);
            if (value % 75 == 0)
            {
                int idx = Random.Range(0, DropObjects.Count);
                DropObjects[idx].useGravity = true;
                DropObjects.RemoveAt(idx);
                Debug.Log(idx + "물건 떨어짐");
            }
        }
        ControlLights[0].intensity = Mathf.Lerp(ControlLights[0].intensity, LitIntensity1 ,0.3f);
        //ControlLights[1].intensity = Mathf.Lerp(ControlLights[1].intensity, LitIntensity2, 0.3f);

    }

}
