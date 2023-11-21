using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : UIEffect
{
    public static ScreenTransition m_Instance;

    public GameObject LoadingIcon;
    public GameObject CakeIcon;
    public bool m_bStartScreen = true;
    bool m_bEndScreen;

    delegate void Updating();
    Updating m_Updating;

    public delegate void PerformFunc();//효과 끝난후 해야하는 함수
    public PerformFunc m_PerformFunc;
    // Start is called before the first frame update
    void Awake()
    {
        var duplicated = FindObjectsOfType<ScreenTransition>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == m_Instance)
            {
                m_Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
        m_Image.material.SetVector("g_vColor", m_Color);
    

        gameObject.SetActive(false);
        
    }

    public static ScreenTransition Instance
    {
        get
        {
            if (null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_Updating();
    }
    void Empty()
    {
        LoadingIcon.transform.Rotate(Vector3.forward * -180f * Time.deltaTime);
    }
    public void StartScreen(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("스크린호출!");
        m_fRatioSour = 1f;
        m_fRatioDest = 0f;

        m_fPassedTime = 0f;
        m_bEndScreen = false;

        m_Updating = LerpRatio;
    }

    public void StartScreen()
    {
        Debug.Log("스크린호출!");
        m_fRatioSour = 1f;
        m_fRatioDest = 0f;

        m_fPassedTime = 0f;
        m_bEndScreen = false;

        m_Updating = LerpRatio;
    }
    public void EndScreen()
    {
        m_fRatioSour = 0f;
        m_fRatioDest = 1f;

        m_fPassedTime = 0f;
        m_Updating = LerpRatio;
        m_bEndScreen = true;

        LoadingIcon.transform.rotation = Quaternion.identity;

        gameObject.SetActive(true);
    }
    public float Get_TransitionTime()
    {
        return m_fTotalTime+1f;
    }
    void LerpRatio()
    {
        m_fPassedTime = Mathf.Min(m_fPassedTime + Time.deltaTime, m_fTotalTime);

        float Ratio = EasingUtility.LerpToType(m_fRatioSour, m_fRatioDest, m_fPassedTime, m_fTotalTime,m_eLerpType);

        m_Image.material.SetFloat("g_fRatio", Ratio);

        LoadingIcon.transform.localScale = new Vector3(Ratio, Ratio, 1f);
        CakeIcon.transform.localScale = new Vector3(Ratio, Ratio, 1f);
        LoadingIcon.transform.Rotate(Vector3.forward*-180f * Time.deltaTime);

        if (Mathf.Abs(m_fPassedTime - m_fTotalTime) < Mathf.Epsilon)
        {
            //m_PerformFunc();
            m_Updating = Empty;
            if (m_bEndScreen == false)
            {
                gameObject.SetActive(false);
            }
            else
            {
                SceneManager.sceneLoaded -= StartScreen;
            }
        }
    }

}
