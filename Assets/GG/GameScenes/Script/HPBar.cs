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
        if (GameMgr.Instance.m_bInGame == true)
        {

            if (!m_PV.IsMine)
            {
                m_MainCamTransform = Camera.main.transform;
                m_Image.material = Instantiate(InstanceMaterial);
                gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);

        }
        else
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vLocalPlayerPos = GameMgr.Instance.m_LocalPlayer.transform.position;
        //Vector3 vThisPos = transform.position;

        //Vector3 vDistance = vLocalPlayerPos - vThisPos;

        //float fLength = Vector3.Magnitude(vDistance);
        //if()


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
        m_Image.material.SetTexture("_MainTex", m_Image.mainTexture);
        m_Image.material.SetVector("vColor",m_Image.color);
    }

}
