using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RewardUI : MonoBehaviour
{
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Exp;
    public TextMeshProUGUI Record;

    public bool m_bSingleMode = false;

    float fDestGold;
    float fDestExp;
    float fSourGold;
    float fSourExp;

    float fDestTime;
    float fSourTime;

    public void Get_Reward(int gold, int exp)
    {
        fDestGold = (float)gold;
        fDestExp = (float)exp;
    }
    public void Get_Reward(float gold, float exp, float Time)
    {
        fDestGold = gold;
        fDestExp = exp;
        fDestTime = Time;
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

        if(m_bSingleMode)
        {
            if (fSourTime < fDestTime)
                fSourTime += 70*Time.deltaTime;
            int Min = Mathf.Max(0, (int)fSourTime / 60);
            int Sec = Mathf.Max(0, (int)fSourTime % 60);

            string szMin = string.Format("{0:D2}", Min);
            string szSec = string.Format("{0:D2}", Sec);
            Record.text = szMin + ":" + szSec;
        }
    }
}
