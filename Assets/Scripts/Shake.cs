using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeTime;//= 1.0f;
    public float shakeSpeed;// = 2.0f;
    public float shakeAmount;// = 1.0f;

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        //EarthQuake();
    }
    public void FIrstShake()
    {
        shakeTime = 3f;
        Invoke("StartShake", 3f);
    }

    public void EarthQuake()
    {
        if (PuzzleMgr.instance.passedPuzzle[2] == 0)
        {
            InvokeRepeating("StartShake", 1f, 10f);
        }
    }

    public void StartShake()
    {
        shakeTime = Random.Range(2f, 5f);
        shakeAmount = Random.Range(3f, 8f);
        StartCoroutine(ShakeCoroutine());
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

        cam.localPosition = originPosition;
    }
}