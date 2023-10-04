using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PuzzleMgr : MonoBehaviour
{
    public static PuzzleMgr instance;
    public int[] passedPuzzle = new int[3] { 1, 1, 1 };
    public bool[] valvePuzzle = new bool[2] { false, false };
    public GameObject light1, light2;
    public GameObject book;
    public Material[] mat = new Material[2];

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

        passedPuzzle[2] = 1;
    }

    private void Update()
    {
        if (valvePuzzle[0] && valvePuzzle[1])
        {
            light1.GetComponent<MeshRenderer>().material = mat[0];
            light2.GetComponent<MeshRenderer>().material = mat[1];
            book.SetActive(true);
            passedPuzzle[0] = 0;
        }
        else
        {
            light1.GetComponent<MeshRenderer>().material = mat[1];
            light2.GetComponent<MeshRenderer>().material = mat[0];
            book.SetActive(false);
            passedPuzzle[0] = 1;
        }
    }
}
