using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEffect : UIEffect
{
    public Sprite m_MaskingImage;
    public GameObject ActivateParticle;

    private StoreItem m_Item;

    private delegate void CalculateEffectValue();
    CalculateEffectValue m_CalcValue;

    private float m_fHighlighting;
    
    // Start is called before the first frame update
    void Start()
    {
        m_CalcValue = Empty;
        m_fCurrRatio = 1f;
    }

    public void Set_Item(StoreItem iInput)
    {
        m_Item = iInput;
        m_Item.Set_EffectScript(this);
    }
    public void Set_IconImage(Image IconImage)
    {
        m_Image.sprite = IconImage.sprite;
    }

    // Update is called once per frame
    void Update()
    {       
        m_CalcValue();

        m_Image.material.SetTexture("_CoolTimeMaskingTex", m_MaskingImage.texture);
        m_Image.material.SetTexture("_MainTex", m_Image.mainTexture);
        m_Image.material.SetFloat("g_fLerpRatio", m_fCurrRatio);
        m_Image.material.SetFloat("g_fHighlightingRatio", m_fHighlighting/0.3f);
        m_Image.material.SetVector("g_vColor", m_Color);
        m_Image.material.SetVector("g_vOriginColor", m_vOriginColor);
        //대충 마스킹 관련 코드 적어두기
    }

    public void Use_Item()
    {
        m_CalcValue = CoolTime;
        m_fCurrRatio = 0f;
        m_fHighlighting = 0f;
        ActivateParticle.SetActive(false);

    }
    public void Activate_Item()
    {
        ActivateParticle.SetActive(true);
    }
    void Empty()
    {

    }
    void CoolTime()
    {
        m_fCurrRatio = m_Item.Get_CoolTime_Ratio();

        if (m_fCurrRatio >= 1f)
        {
            m_fHighlighting = 0.3f;
            m_CalcValue = HighLighting;
        }
        
    }
    void HighLighting()
    {
        m_fHighlighting = m_fHighlighting - Time.deltaTime;
        
        if(m_fHighlighting < 0f)
        {
            m_fHighlighting = 0f;

            m_CalcValue = Empty;
        }
        
    }
}
