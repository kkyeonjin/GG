using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEffect : MonoBehaviour
{
    public bool m_bIsFloating;
    public RectTransform m_MyRectTransform;

    private float m_fTotalTime =0;
    private float m_fStartYPos;
    // Start is called before the first frame update
    void Start()
    {
        m_fStartYPos = m_MyRectTransform.position.y;
    }

    private void OnDisable()
    {
        m_fTotalTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(m_bIsFloating)
        {
            m_fTotalTime += 2f*Time.deltaTime;
            m_MyRectTransform.position = new Vector3(m_MyRectTransform.position.x, m_fStartYPos + 8f*Mathf.Sin(m_fTotalTime), m_MyRectTransform.position.z);
        }
    }
}
