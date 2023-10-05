using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmplitude = 1.0f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 1.0f;         // Cinemachine Noise Profile Parameter

    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            if (Earthquake.isQuake)
            {
                Debug.Log("camera is shaking");
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
            }
        }
    }
}