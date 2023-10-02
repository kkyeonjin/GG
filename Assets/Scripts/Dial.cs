using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dial : MonoBehaviour
{

    public TMP_Text dialText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callBtn()
    {
        if(dialText.text == "131")
        {
            Debug.Log("correct");
        }

        else
        {
            Debug.Log(".......");
        }

        dialText.text = "";
    }

    public void numberBtn(string num)
    {
        if(dialText.text.Length < 8)
        {
            dialText.text += num;
        }
    }

}
