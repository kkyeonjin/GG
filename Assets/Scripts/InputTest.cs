using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputTest : MonoBehaviour
{
    public TMP_InputField inputText;
    public GameObject minimap;

    public void Search()
    {
        if (inputText.text == "¿Ã»≠∑Œ")
        {
            minimap.SetActive(true);
        }
        inputText.text = " ";
    }
}
