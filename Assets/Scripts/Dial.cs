using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dial : MonoBehaviour
{
    public TMP_Text dialText;
    public GameObject callPanel;
    public TMP_Text callText;

    public void callBtn()
    {
        if(dialText.text == "131")
        {
            Debug.Log("correct");
            CallText();
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

    public void CallText()
    {
        callPanel.SetActive(true);
        int temp = PuzzleMgr.instance.passedPuzzle[0] + PuzzleMgr.instance.passedPuzzle[1] + PuzzleMgr.instance.passedPuzzle[2];
        callText.text = "�α� && ������ �Ը�5.3�� ������ �߻��߽��ϴ�.\n��� �Ϸ���� " + temp + "�ܰ� ���ҽ��ϴ�.";
        Invoke("CallClose", 3.0f);
    }

    public void CallClose()
    {
        callPanel.SetActive(false);
    }

}
