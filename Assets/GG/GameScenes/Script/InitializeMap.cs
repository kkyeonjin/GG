using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("¿Ã¥œº»∂Û¿Ã¬° ∏ !");
        GameMgr.Instance.Load_LocalPlayer();
        GameMgr.Instance.Set_PlayerPos();
        GameMgr.Instance.Set_Camera();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
