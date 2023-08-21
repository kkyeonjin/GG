using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEffect : UIEffect
{
    public Image m_MaskingImage;

    private StoreItem m_Item;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Set_Item(StoreItem iInput)
    {
        m_Item = iInput;

    }

    // Update is called once per frame
    void Update()
    {
        
        m_fCurrRatio = m_Item.Get_CoolTime_Ratio();

        m_Image.material.SetFloat("g_fLerpRatio", m_fCurrRatio);
        //대충 마스킹 관련 코드 적어두기
    }
}
