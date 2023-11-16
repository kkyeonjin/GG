using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardUI : MonoBehaviour
{
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Exp;

    float fDestGold;
    float fDestExp;
    float fSourGold;
    float fSourExp;

    public void Get_Reward(int gold, int exp)
    {
        fDestGold = (float)gold;
        fDestExp = (float)exp;
    }
    

    void Start()
    {
        Gold.text = "0";
        Exp.text = "0";
    }

    
    void Update()
    {
        if(fSourGold < fDestGold)
            fSourGold = Mathf.Max(fDestGold , fSourGold+(int)(100f*Time.deltaTime));
        if(fSourExp < fDestExp)
            fSourExp = Mathf.Max(fDestExp, fSourExp + (int)(100f * Time.deltaTime));

        Gold.text = "" + (int)fSourGold;
        Exp.text = "" + (int)fSourExp;
    }
}
