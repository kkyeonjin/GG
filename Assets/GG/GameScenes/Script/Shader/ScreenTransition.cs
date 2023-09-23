using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : UIEffect
{
    public static ScreenTransition m_Instance;

    public bool m_bStartScreen = true;
    bool m_bEndScreen;

    delegate void Updating();
    Updating m_Updating;

    public delegate void PerformFunc();//ȿ�� ������ �ؾ��ϴ� �Լ�
    public PerformFunc m_PerformFunc;
    // Start is called before the first frame update
    void Awake()
    {
        var duplicated = FindObjectsOfType<ScreenTransition>();

        if (duplicated.Length > 1)
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
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

    }
    public void StartScreen(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("��ũ��ȣ��!");
        m_fRatioSour = 1f;
        m_fRatioDest = 0f;

        m_fPassedTime = 0f;
        m_bEndScreen = false;

        m_Updating = LerpRatio;
    }

    public void StartScreen()
    {
        Debug.Log("��ũ��ȣ��!");
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
