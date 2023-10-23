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
    public GameObject manual1, manual2, manual3;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

        passedPuzzle[2] = 1;
    }
    public void Manual1Unlock()
    {
        InfoHandler.Instance.Unlock_Manual(InfoHandler.HOUSE.GAS);
        manual1.SetActive(true);
        Invoke("Manual1Close", 2f);
    }

    public void Manual1Close()
    {
        manual1.SetActive(false);
    }

    public void Manual2Unlock()
    {
        InfoHandler.Instance.Unlock_Manual(InfoHandler.HOUSE.PACKING);
        manual2.SetActive(true);
        Invoke("Manual2Close", 2f);
    }

    public void Manual2Close()
    {
        manual2.SetActive(false);
    }

    public void Manual3Unlock()
    {
        InfoHandler.Instance.Unlock_Manual(InfoHandler.HOUSE.TABLE);
        Invoke("Manual3Open", 1f);
        Invoke("Manual3Close", 2f);
    }

    public void Manual3Open()
    {
        manual3.SetActive(true);
    }

    public void Manual3Close()
    {
        manual3.SetActive(false);
    }

    private void Update()
    {
        if (valvePuzzle[0] && valvePuzzle[1])
        {
            light1.GetComponent<MeshRenderer>().material = mat[0];
            light2.GetComponent<MeshRenderer>().material = mat[1];
            book.SetActive(true);
            if (passedPuzzle[0] == 1)
            {
                Manual1Unlock();
                passedPuzzle[0] = 0;
            }
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
