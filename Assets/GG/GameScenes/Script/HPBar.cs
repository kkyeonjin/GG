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

    void Awake()
    {
        m_MainCamTransform = Camera.main.transform;

        m_Image.material = Instantiate(InstanceMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PV)
            m_PV.RPC("Update_HPBar", RpcTarget.All);
    }

    private void OnPreRender()
    {

    }

    [PunRPC]
    void Update_HPBar()
    {
        m_fHPRatio = m_Status.Get_HP() / m_Status.Get_MaxHP();
        m_Image.material.SetFloat("fRatio", m_fHPRatio);
    }

}
