using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEffect : UIEffect
{
    public Sprite m_MaskingImage;
    public GameObject ActivateParticle;
    public SpriteRenderer m_IconImage;

    public bool m_bInstantiateMaterial = true;

    private StoreItem m_Item;

    private delegate void CalculateEffectValue();
    CalculateEffectValue m_CalcValue;

    private float m_fHighlighting=0;

    private void Awake()
    {
        if(m_bInstantiateMaterial)
            m_IconImage.material = Instantiate(m_Material);
        m_fRatioSour = 0f;

       // m_fOriginScale = m_Image.transform.localScale.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CalcValue = Empty;
        m_fCurrRatio = 1f;
        if(ActivateParticle != null)
            ActivateParticle.SetActive(false);
    }

    public void Set_Item(StoreItem iInput)
    {
        m_Item = iInput;
        m_Item.Set_EffectScript(this);
    }
    public void Set_IconImage(Image IconImage)
    {
        m_IconImage.sprite = IconImage.sprite;
    }

    public void Sharing_Material(Material shared)
    {
        m_IconImage.material = shared;
    }

    public Material Get_Material()
    {
        return m_IconImage.material;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_bInstantiateMaterial == false)
            return;

        m_CalcValue();

        m_IconImage.material.SetTexture("_CoolTimeMaskingTex", m_MaskingImage.texture);
        m_IconImage.material.SetTexture("_MainTex", m_IconImage.sprite.texture);
        m_IconImage.material.SetFloat("g_fLerpRatio", m_fCurrRatio);
        m_IconImage.material.SetFloat("g_fHighlightingRatio", m_fHighlighting/0.3f);
        m_IconImage.material.SetVector("g_vColor", m_Color);
        m_IconImage.material.SetVector("g_vOriginColor", m_vOriginColor);
        //대충 마스킹 관련 코드 적어두기
    }

    public void Use_Item()
    {
        m_fCurrRatio = 0f;
        m_fHighlighting = 0f;
        ActivateParticle.SetActive(false);

    }
    public void Activate_Item(bool activate = true)
    {
        ActivateParticle.SetActive(activate);
    }
    void Empty()
    {

    }

}
