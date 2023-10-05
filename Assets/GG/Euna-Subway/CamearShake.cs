using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera shakeCamera;
    Vector3 cameraPos;

    [SerializeField]
    [Range(0.01f, 0.1f)] float shakeRange = 0.05f;
    [SerializeField]
    [Range(0.1f, 1f)] float duration = 0.5f;

    public void Shake()
    {
        cameraPos = shakeCamera.transform.position;
        InvokeRepeating("StartShake", 10f, 0.005f);
    }

    void StartShake()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = shakeCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        shakeCamera.transform.position = cameraPos;
    }

    void StopShake()
    {
        if (Earthquake.isQuakeStop)
        {
            CancelInvoke("StartShake");
            shakeCamera.transform.position = cameraPos;
        }
    }

}