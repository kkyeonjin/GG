using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public RectTransform m_MyRect;

    Vector3 m_LoadedPos;
    bool m_bIsHold = false;


    // Start is called before the first frame update
    void Start()
    {
        m_LoadedPos = m_MyRect.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bIsHold)
        {
            m_MyRect.position = Input.mousePosition;
        }
    }

    public void OnButtondown()
    {
        m_bIsHold = true;
        Debug.Log("클릭");
    }

    public void OnButtonUp()
    {
        m_bIsHold = false;
        m_MyRect.position = m_LoadedPos;
        Debug.Log("클릭");

    }

}
