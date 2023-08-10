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

    private delegate void CamFunc();
    private CamFunc m_CameraFunc;

    Quaternion tempRot;


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
            m_CameraFunc = new CamFunc(MultiMode);

        tempRot = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Get_MouseMovement();
        m_CameraFunc();


        Quaternion newRot = transform.localRotation;
        if (tempRot == newRot) return;
        else
        {
            Debug.Log(newRot);
            tempRot = newRot;
        }
    }

    private void MultiMode()
    {
        
        if (m_CamTransform.Follow != null)
        {
            m_TargetTransform = m_CamTransform.Follow;
        }
        if (m_TargetTransform != null)
        {
            transform.position = new Vector3(0f, m_vOffset.y, 0f) + m_TargetTransform.position + m_vOffset.z * transform.forward + m_vOffset.y * transform.up;

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
