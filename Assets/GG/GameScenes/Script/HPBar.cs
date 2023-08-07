using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class HPBar : MonoBehaviour
{
    public Image m_Image;
    public CharacterStatus m_Status;
    public PhotonView m_PV;
    public Material InstanceMaterial;

    Transform m_MainCamTransform;
    float m_fHPRatio;

    void Start()
    {
        m_MainCamTransform = Camera.main.transform;

        m_Image.material = Instantiate(InstanceMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PV)
            m_PV.RPC("Update_HPBar", RpcTarget.All);
        m_Image.material.SetFloat("fRatio", m_fHPRatio);
        
    }
    //쉐이더 변수 값 지정하는 타이밍. 만약에 일반 hlsl과 같다면 같은 쉐이더를 공유한다면 문제 발생
    //
    private void OnPreRender()
    {
        //m_Image.material.SetFloat("g_fRatio", m_fHPRatio);
    }

    [PunRPC]
    void Update_HPBar()
    {
        m_fHPRatio = m_Status.Get_HP() / m_Status.Get_MaxHP();
        m_Image.fillAmount = m_fHPRatio;
    }

}
