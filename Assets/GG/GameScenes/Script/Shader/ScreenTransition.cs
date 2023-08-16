using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : UIEffect
{
    public bool m_bStartScreen = true;
    bool m_bEndScreen;

    delegate void Updating();
    Updating m_Updating;

    public delegate void PerformFunc();//효과 끝난후 해야하는 함수
    public PerformFunc m_PerformFunc;
    // Start is called before the first frame update
    void Awake()
    {
        m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
        if (m_bStartScreen)
            StartScreen();
        else
            gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Updating();
    }

    public void StartScreen()
    {
        Debug.Log("스크린호출!");
        m_fRatioSour = 1f;
        m_fRatioDest = 0f;

        m_fPassedTime = 0f;
        m_bEndScreen = false;

        m_Updating += LerpRatio;
    }
    public void EndScreen()
    {
        m_fRatioSour = 0f;
        m_fRatioDest = 1f;

        m_fPassedTime = 0f;
        m_Updating += LerpRatio;
        m_bEndScreen = true;

        gameObject.SetActive(true);
    }
    public float Get_TransitionTime()
    {
        return m_fTotalTime+1f;
    }
    void LerpRatio()
    {
        m_fPassedTime = Mathf.Min(m_fPassedTime + Time.deltaTime, m_fTotalTime);
        m_Image.material.SetFloat("g_fRatio", EasingUtility.Linear(m_fRatioSour, m_fRatioDest, m_fPassedTime, m_fTotalTime));
        
        if (Mathf.Abs(m_fPassedTime - m_fTotalTime) < Mathf.Epsilon)
        {
            //m_PerformFunc();
            if (m_bEndScreen == false)
            {
                m_Updating -= LerpRatio;
                gameObject.SetActive(false);
            }
        }
    }

}
