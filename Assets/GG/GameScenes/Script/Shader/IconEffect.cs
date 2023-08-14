using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IconEffect : MonoBehaviour
{
 
    public bool m_bIsFloating;
    public RectTransform m_MyRectTransform;

    //stat effect
    public bool m_bIsStatbar;
    public Image m_MyImage;
    public Material m_InstantiateMaterial;
    public float m_fLengthRatio;
    public Color m_Color;
    private float m_fRatioSour;

    private float m_fTotalTime =0;
    private float m_fStartYPos;
    // Start is called before the first frame update
    void Start()
    {
        if(m_bIsFloating)
            m_fStartYPos = m_MyRectTransform.position.y;

        if(m_bIsStatbar)
        {
            m_MyImage.material = Instantiate(m_InstantiateMaterial);
            m_fLengthRatio = m_fLengthRatio / 150f;
        }
    }

    private void OnDisable()
    {
        m_fTotalTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(m_bIsFloating)
        {
            m_fTotalTime += 2f*Time.deltaTime;
            m_MyRectTransform.position = new Vector3(m_MyRectTransform.position.x, m_fStartYPos + 8f*Mathf.Sin(m_fTotalTime), m_MyRectTransform.position.z);
        }
        if(m_bIsStatbar)
        {
            m_fRatioSour = Mathf.Lerp(m_fRatioSour, m_fLengthRatio, 0.3f);
            
            m_MyImage.material.SetFloat("g_fLerpRatio", m_fRatioSour);
            m_MyImage.material.SetVector("g_vColor", m_Color);
        }
    }
}
