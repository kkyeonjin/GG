using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public static Shake instance;
    public float shakeTime;//= 1.0f;
    public float shakeSpeed;// = 2.0f;
    public float shakeAmount;// = 1.0f;

    public Transform HideCam1, HideCam2, HideCam3, MainCam;
    Transform cam;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
    }

    void Update()
    {
        //EarthQuake();
    }
    public void FIrstShake()
    {
        shakeTime = 3f;
        if (PuzzleMgr.instance.activeCam[0] == true)
        {
            cam = MainCam;
        }
        else if (PuzzleMgr.instance.activeCam[1] == true)
        {
            cam = HideCam1;
        }
        else if (PuzzleMgr.instance.activeCam[2] == true)
        {
            cam = HideCam2;
        }
        else if (PuzzleMgr.instance.activeCam[3] == true)
        {
            cam = HideCam3;
        }

        Invoke("StartShake", 1f);
    }

    public void EarthQuake()
    {
        cam = MainCam;
        InvokeRepeating("StartShake", 1f, 10f);
        
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

        cam.localPosition = Vector3.Lerp(cam.localPosition, originPosition, Time.deltaTime * shakeSpeed);
    }
}