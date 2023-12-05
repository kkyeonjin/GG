using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    public Image m_Image;
    public Material m_Material;
    public Color m_Color;
    public Color m_vOriginColor;
    public float m_fTotalTime = 1f;
    public float m_fScale = 0.1f;
    public EasingUtility.EASING_TYPE m_eLerpType;

    public bool m_bGetPassTime;

    public GameObject[] m_ConnectedModel;

    public float m_fRatioDest= 0f;
    public float m_fRatioSour= 1f;
    public bool m_bUseShader = true;
    protected float m_fPassedTime = 0f;
    protected float m_fOriginScale = 1f;

    protected float m_fCurrRatio = 0f;


    void Awake()
    {
        if(m_bUseShader)
            m_Image.material = Instantiate(m_Material);
        //m_fRatioSour = 0f;

        m_fOriginScale = m_Image.transform.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_bGetPassTime == false)
            m_fPassedTime = Mathf.Min(m_fPassedTime + Time.deltaTime, m_fTotalTime);

        m_fCurrRatio = EasingUtility.LerpToType(m_fRatioSour,m_fRatioDest, m_fPassedTime, m_fTotalTime,m_eLerpType);


        m_Image.transform.localScale = new Vector3(m_fOriginScale + m_fCurrRatio * m_fScale, m_fOriginScale + m_fCurrRatio * m_fScale, m_fOriginScale);

        if (m_bUseShader)
        {
            m_Image.material.SetFloat("g_fLerpRatio", m_fCurrRatio);
            m_Image.material.SetVector("g_vColor", m_Color);
            m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
        }
        else 
        {
            m_Image.fillAmount = m_fCurrRatio;
        }
    }

    private void OnPreRender()
    {
        
    }
    public void Lerp_Increasing()
    {
        m_fPassedTime = 0f;

        m_fRatioDest = 1f;
        m_fRatioSour = m_fCurrRatio;

    }

    public void Lerp_Decreasing()
    {
        m_fPassedTime = 0f;

        m_fRatioDest = 0f;
        m_fRatioSour = m_fCurrRatio;
    }

    public void MousePointer_In()
    {
        Lerp_Increasing();

        if (m_ConnectedModel.Length  >0)
        {
            foreach(GameObject it in m_ConnectedModel)
                it.SetActive(true);
        }
    }

    public void MousePointer_Out()
    {
        Lerp_Decreasing();

        if (m_ConnectedModel.Length > 0)
        {
            foreach (GameObject it in m_ConnectedModel)
                it.SetActive(false);
        }
    }
    public void Set_Ratio(float fRatio)
    {
        m_fRatioSour = m_fCurrRatio;
        m_fRatioDest = fRatio;
        m_fPassedTime = 0f;
    }

    public void Get_PassedTime(float fTime)
    {
        m_fPassedTime = fTime;
    }
}
