using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;
public class FirstGameStart : MonoBehaviour
{
    public TMP_InputField Nickname;

    // Start is called before the first frame update
    void Start()
    {
        if(System.IO.File.Exists(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "PlayerInfo")))
        {
            SceneManager.LoadScene("MenuUI");
        }
        
    }

    public void Initialize_Player()
    {
        InfoHandler.Initizlize_Player(Nickname.text);
        SceneManager.LoadScene("MenuUI");
    }

}
