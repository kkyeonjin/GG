using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    public Image m_Image;
    public Color m_Color;
    public Material m_Material;

    public float m_fScaleDest= 0f;
    public float m_fScaleSour= 1f;
    public float m_fLerpRatio= 0.35f;

    public GameObject m_ConnectedModel;

    private float m_fRatio = 0f;
    private Vector4 m_vColor;
    private Vector4 m_vOriginColor;

    void Start()
    {
        m_vColor.x = m_Color.r;
        m_vColor.y = m_Color.g;
        m_vColor.z = m_Color.b;
        m_vColor.w = m_Color.a;

        m_vOriginColor = new Vector4(0.6f, 0.6f, 0.6f, 0.6f);
        m_Image.material = Instantiate(m_Material);
        m_fScaleSour = 0f;

    }

    // Update is called once per frame
    private void Update()
    {
        
        m_fRatio = Mathf.Lerp(m_fScaleSour, m_fScaleDest, m_fLerpRatio);
        m_fScaleSour = m_fRatio;
        
        m_Image.material.SetFloat("g_fLerpRatio", m_fRatio);
        m_Image.material.SetVector("g_vColor", m_vColor);
        m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
       
        m_Image.transform.localScale = new Vector3(1f + m_fRatio*0.1f, 1f + m_fRatio*0.1f, 1f);
    }

    private void OnPreRender()
    {
        
    }

    public void MousePointer_In()
    {
        m_fScaleDest = 1f;
        m_ConnectedModel.SetActive(true);
    }

    public void MousePointer_Out()
    {
        m_fScaleDest = 0f;
        m_ConnectedModel.SetActive(false);
    }
}
