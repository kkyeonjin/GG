using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BagManage : MonoBehaviour
{
    public static BagManage instance;

    public int totalValue;
    public int totalWeight;

    public TMP_Text tvText, twText;

    public Image phoneIcon;

    private void Update()
    {
        tvText.text = totalValue.ToString();
        twText.text = totalWeight.ToString();

        if(totalWeight == 131)
        {
            phoneIcon.gameObject.SetActive(true);
            if(PuzzleMgr.instance.passedPuzzle[1] == 1)
            {
                PuzzleMgr.instance.Manual2Unlock();
                PuzzleMgr.instance.passedPuzzle[1] = 0;
            }
            //해당 매뉴얼 해금 코드

        }
        else
        {
            phoneIcon.gameObject.SetActive(false);
            PuzzleMgr.instance.passedPuzzle[1] = 1;
        }

    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        totalValue = 0;
        totalWeight = 0;
        Debug.Log(totalValue);
    }
}
