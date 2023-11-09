using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float ShakeAmplitude = 1.0f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 1.0f;         // Cinemachine Noise Profile Parameter

    public Earthquake earthquake;

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

    public void shakeCamera()
    {
        virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
        virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
    }

    public void shakeCameraStop()
    {
        virtualCameraNoise.m_AmplitudeGain = 0f;
    }
}