using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChange : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canvasCamera;
    public GameObject Puzzlecanvas, Maincanvas;
    public bool canvasOn; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PuzzleOn()
    {
        canvasOn = !canvasOn;
        canvasCamera.SetActive(canvasOn);
        Puzzlecanvas.SetActive(canvasOn);
        Maincanvas.SetActive(!canvasOn);

    }
}
