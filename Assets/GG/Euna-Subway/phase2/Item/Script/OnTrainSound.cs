using UnityEngine;

public class OnTrainSound : MonoBehaviour
{
    //백그라운드 사운드
    public AudioSource audioSrcTrain;
    public AudioClip subwayNoise;
    public AudioClip emergencyAlarm;
    public AudioClip earthquakeNoise;
    public AudioClip phase1BGM;

    private void Awake()
    {
        audioSrcTrain = GetComponent<AudioSource>();
        audioSrcTrain.clip = subwayNoise;
    }

    private void Start()
    {
        audioSrcTrain.Play();
    }




}