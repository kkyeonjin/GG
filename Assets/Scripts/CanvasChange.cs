using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasCamera;
    public GameObject canvas;
    public bool canvasOn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        canvasOn = !canvasOn;
        canvasCamera.SetActive(canvasOn);
        canvas.SetActive(canvasOn);

    }
}
