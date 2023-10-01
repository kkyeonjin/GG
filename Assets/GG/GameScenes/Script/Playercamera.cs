using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playercamera : MonoBehaviour
{
    public Vector3 m_vOffset;
    public float m_fRotateSpeed = 250f; 
    public CinemachineVirtualCamera m_CamTransform;
    public Transform m_TargetTransform;

    private float m_fXRotate, m_fYRotate;
    private float m_XTotalRot, m_YTotalrot;
    private float m_fCamDist;
    private Vector3 m_vDir;

    private delegate void CamFunc();
    private CamFunc m_CameraFunc;

    RaycastHit hitinfo;

    void Start()
    {

        if (null == m_CamTransform)//single mode
        {
            m_CameraFunc = new CamFunc(SingleMode);
            transform.right = m_TargetTransform.right;
            transform.up = m_TargetTransform.up;
            transform.forward = m_TargetTransform.forward;
        }
        else
        {
            m_CameraFunc = new CamFunc(MultiMode);
            m_fCamDist = Mathf.Sqrt(m_vOffset.y * m_vOffset.y + m_vOffset.z * m_vOffset.z);
            m_vDir = new Vector3(0, m_vOffset.y, m_vOffset.z).normalized;
        }
    }

    private void Update()
    {
       if(m_TargetTransform != null)
        {
            Vector3 ray_Dir = m_vOffset.z * transform.forward + m_vOffset.y * transform.up;//얘 여기에 놓으면 튐
            Physics.Raycast(m_TargetTransform.position + new Vector3(0f, m_vOffset.y, 0f), ray_Dir, out hitinfo, m_fCamDist);
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Get_MouseMovement();
        m_CameraFunc();
    }

    private void MultiMode()
    {

        if (m_CamTransform.Follow != null)
        {
            m_TargetTransform = m_CamTransform.Follow;
        }
        if (m_TargetTransform != null)
        {
            transform.position = new Vector3(0f, m_vOffset.y, 0f) + m_TargetTransform.position/* + m_vOffset.z * transform.forward + m_vOffset.y * transform.up*/;
             if (hitinfo.point != Vector3.zero)//레이케스트 성공시
            {
                //point로 옮긴다.
                transform.position = hitinfo.point;
                //카메라 보정
                transform.Translate(m_vDir * -1 * 3f);
            }
            else
            {
                transform.position = new Vector3(0f, m_vOffset.y, 0f) + m_TargetTransform.position + m_vOffset.z * transform.forward + m_vOffset.y * transform.up;
            }
        }
    }

    private void SingleMode()
    {
        transform.position = new Vector3(0f,m_vOffset.y,0f) + m_TargetTransform.position + m_vOffset.z * transform.forward + m_vOffset.y * transform.up;

    }

    private void Get_MouseMovement()
    {
       
        m_fXRotate = -Input.GetAxis("Mouse Y") * Time.deltaTime * m_fRotateSpeed;
        m_fYRotate = Input.GetAxis("Mouse X") * Time.deltaTime * m_fRotateSpeed;

        m_YTotalrot += m_fYRotate;
        m_XTotalRot += m_fXRotate;

        m_XTotalRot = Mathf.Clamp(m_XTotalRot, -55, 55);

        transform.eulerAngles = new Vector3(m_XTotalRot, m_YTotalrot, 0);
    }
}
