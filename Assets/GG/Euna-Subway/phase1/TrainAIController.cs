using Photon.Pun.Demo.Cockpit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrainAIController : MonoBehaviour 
{
    private bool isQuake;
    private bool isQuakeStop;
    //AI Animator
    public GameObject SittingPassengers;
    private Animator[] SittingPassengers_Animators;
    public GameObject StandingPassengers;
    private Animator[] StandingPassengers_Animators;

    private void Awake()
    {
        SittingPassengers_Animators = SittingPassengers.GetComponentsInChildren<Animator>();
        StandingPassengers_Animators = StandingPassengers.GetComponentsInChildren<Animator>();
    }
    private void Update()
    {
        if(!isQuake && Phase1Mgr.Instance.earthquake.isQuake)
        {
            ChangeAnimators("isQuake");
            isQuake = true;
            return;
        }
        if(!isQuakeStop && Phase1Mgr.Instance.earthquake.isQuakeStop)
        {
            ChangeAnimators("isQuakeStop");
            isQuakeStop = true;
            return;
        }
    }
    public void ChangeAnimators(string triggerString)
    {
        foreach (Animator anim in SittingPassengers_Animators)
        {
            anim.SetTrigger(triggerString);
        }
        foreach (Animator anim in StandingPassengers_Animators)
        {
            anim.SetTrigger(triggerString);
        }
    }
    /*
    //List<Animator> animators;
    public Animator Sitting;
    public Animator SittingTalking;
    public Animator Standing;

    private void Awake()
    {
        animators.Add(Sitting);
        animators.Add(SittingTalking);  
        animators.Add(Standing);
    }


    private void Update()
    {
        //0. 지하철 탑승 중 (Entry)

        //1. 지진 발생 : 놀라기 / 놀라기 / 넘어지기

        //2. 지진 발생 : 붙잡기 

        //3. 


        if(Phase1Mgr.Instance.earthquake.isQuake && !Phase1Mgr.Instance.earthquake.isQuakeStop)
        {
            ChangeAnimator("isQuake");
        }
        if(Phase1Mgr.Instance.earthquake.isQuake && Phase1Mgr.Instance.earthquake.isQuakeStop)
        {
            ChangeAnimator("isQuakeStop");
        }

    }
    */

}