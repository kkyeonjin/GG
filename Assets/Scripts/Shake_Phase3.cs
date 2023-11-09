using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake_Phase3 : MonoBehaviour
{
    public static Shake_Phase3 instance;
    public float shakeTime;//= 1.0f;
    public float shakeSpeed;
    public float shakeAmount;// = 1.0f;

    public Transform MainCam;
    Transform cam;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

    }

    private void Start()
    {
        MainCam = Camera.main.transform;
        EarthQuake();
    }

    public void FIrstShake()
    {
        cam = MainCam;
        shakeTime = 3f;
        Debug.Log("FirstShake");
        Invoke("StartShake", 0.5f);
    }

    public void EarthQuake()
    {
        StopCoroutine(ShakeCoroutine());
        cam = MainCam;
        Debug.Log("EarthQuake");
        InvokeRepeating("StartShake2", 3f, 8f);
        
    }

    public void StartShake()
    {
        cam = MainCam;
        shakeTime = 5f;
        shakeAmount = 6f;
        StartCoroutine(ShakeCoroutine());
    }

    public void StartShake2()
    {
        shakeTime = Random.Range(2f, 5f);
        shakeAmount = Random.Range(3f, 8f);
        StartCoroutine(ShakeCoroutine2());
    }

    IEnumerator ShakeCoroutine()
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        cam.localPosition = Vector3.Lerp(cam.localPosition, originPosition, Time.deltaTime); // * shakeSpeed);
    }

    IEnumerator ShakeCoroutine2()
    {
        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeTime)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * shakeAmount;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * shakeSpeed);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        cam.localPosition = Vector3.Lerp(cam.localPosition, originPosition, Time.deltaTime); // * shakeSpeed);
    }
}