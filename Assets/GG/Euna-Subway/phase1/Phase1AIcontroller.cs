using Photon.Pun.Demo.Cockpit;
using System.Collections.Generic;
using UnityEngine;

public class Phase1AIcontorller
{
    List<Animator> animators;
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
        if(Earthquake.isQuake && !Earthquake.isQuakeStop)
        {
            ChangeAnimator("isQuake");
        }
        if(!Earthquake.isQuake && Earthquake.isQuakeStop)
        {
            ChangeAnimator("isQuakeStop");
        }
    }

    //action[0] : 시작 시 행동. 
    private void ChangeAnimator(string anim)
    {
        for(int i = 0; i < animators.Count; i++)
        {
            animators[i].SetBool(anim, true);
        }
    }
}