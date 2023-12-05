using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn : MonoBehaviour
{
    public GameObject canvasCam;
    public GameObject canvas;
    // Start is called before the first frame update

    public void BackBtn()
    {
        Debug.Log("Click");
        canvasCam.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
