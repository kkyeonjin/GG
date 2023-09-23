using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public Transform AdjustPosition;
    public enum INTERACT { LEVER, COLUMN, END};

    protected INTERACT m_eType = INTERACT.END;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Get_Type()
    {
        return (int)m_eType;
    }

    public virtual void Interacting(GameObject player)
    {
        player.transform.localRotation = AdjustPosition.localRotation;
        player.transform.position = AdjustPosition.position;
    }

}
