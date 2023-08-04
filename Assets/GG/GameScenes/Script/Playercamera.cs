using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercamera : MonoBehaviour
{
    public Transform m_PlayerTransform;
    public Vector3 m_vOffset;
    public float m_fRotateSpeed = 250f; 

    private float m_fXRotate, m_fYRotate;
    private float m_XTotalRot, m_YTotalRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Get_MouseMovement();
        transform.position = m_PlayerTransform.position + m_vOffset.z*transform.forward + m_vOffset.y * transform.up;

        Vector3 vLookatPosition = m_PlayerTransform.position + new Vector3(0f, 1.5f, 0f);
        transform.LookAt(vLookatPosition);
    }

    private void Get_MouseMovement()
    {
        // m_fXRotate += -Input.GetAxis("Mouse Y") * Time.deltaTime * 0.1f;
        // m_fYRotate += Input.GetAxis("Mouse X") * Time.deltaTime * 0.1f;

        m_fXRotate = -Input.GetAxis("Mouse Y") * Time.deltaTime * m_fRotateSpeed;
        m_fYRotate = Input.GetAxis("Mouse X") * Time.deltaTime * m_fRotateSpeed;

        m_YTotalRot += m_fYRotate;
        m_XTotalRot += m_fXRotate;

        m_XTotalRot = Mathf.Clamp(m_XTotalRot, -90, 90);
        m_YTotalRot = Mathf.Clamp(m_YTotalRot, -90, 90);

        transform.eulerAngles = new Vector3(m_XTotalRot, m_YTotalRot, 0);
    }
}
