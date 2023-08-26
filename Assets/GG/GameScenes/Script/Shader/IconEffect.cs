using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IconEffect : UIEffect
{
 
    public bool m_bIsFloating;
    public RectTransform m_MyRectTransform;

    public float m_fTotalLength;
    public bool m_bIsStatbar;
    public float m_fRatio=0;

    private float m_fStartYPos;
    // Start is called before the first frame update
    void Awake()
    {
        if(m_bIsFloating)
            m_fStartYPos = m_MyRectTransform.position.y;

        if(m_bIsStatbar)
        {
            m_Image.material = Instantiate(m_Material);
            m_fRatioDest = m_fRatio / m_fTotalLength;

            m_fPassedTime = 0;
            m_fRatioSour = 0;
        }
    }

    private void OnDisable()
    {
        m_fPassedTime = 0;
        m_fRatioSour = 0;
    }
    // Update is called once per frame
    private void Update()
    {
        if(m_bIsFloating)
        {
            m_fPassedTime += 2f*Time.deltaTime;
            m_MyRectTransform.position = new Vector3(m_MyRectTransform.position.x, m_fStartYPos + 8f*Mathf.Sin(m_fPassedTime), m_MyRectTransform.position.z);
        }
        if(m_bIsStatbar)
        {
            m_fPassedTime = Mathf.Min(m_fPassedTime + Time.deltaTime, m_fTotalTime);

            m_Image.material.SetFloat("g_fLerpRatio", EasingUtility.CubicOut(m_fRatioSour, m_fRatioDest, m_fPassedTime, m_fTotalTime));
            m_Image.material.SetVector("g_vColor", m_Color);
            m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
        }
    }

    public void Set_LengthRatio(float fRatio)
    {
        m_fRatioDest = fRatio/ m_fTotalLength;

        m_fRatioSour = 0f;
        m_fPassedTime = 0f;
    }

    public void Set_TotalLength(float fLength)
    {
        m_fTotalLength = fLength;
    }
}
