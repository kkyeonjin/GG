using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SingleGameMgr : MonoBehaviour
{
    public static SingleGameMgr m_Instance = null;
     
    public float fResultScreenTime = 10f;
    public float fCeremonyTime = 5f;

    public GameObject ResultScreen;
    public GameObject GameScreen;

    public Player m_LocalPlayer;
    public GameObject m_LocalPlayerObj;

    public float m_fPassedTime;

    public float fDelayStartTime = 2f;

    public TextMeshProUGUI Money;
    public TextMeshProUGUI Exp;
    public TextMeshProUGUI Record;

    public GameObject GameOver;

    private bool m_bPlayerGoalIn = false;

    void Awake()
    {

        var duplicated = FindObjectsOfType<SingleGameMgr>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }


    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public static SingleGameMgr Instance
    {
        get
        {
            if (null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }

    public void Player_GoalIn()//싱글 모드
    {
        Debug.Log("player GoalIn!");
        m_bPlayerGoalIn = true;
        Invoke("Show_ResultScreen", fCeremonyTime);

        int Min = Mathf.Max(0, (int)m_fPassedTime / 60);
        int Sec = Mathf.Max(0, (int)m_fPassedTime % 60);

        string szMin = string.Format("{0:D2}", Min);
        string szSec = string.Format("{0:D2}", Sec);
        Record.text = szMin + ":" + szSec;

    }
   
    //////////인게임에서 쓰일 함수들//././///////
    void Update()
    {
        if (m_bPlayerGoalIn)
            return;

        m_fPassedTime += Time.deltaTime;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void Game_Over()
    {
        GameOver.SetActive(true);
        Invoke("BackToLobby", 5f);

    }
    public void Reward_Player()//게임 끝난후 로비로 돌아감
    {
        //사용한 아이템 개수 Info에 업데이트
        //보상 등 여기서 주면 될듯

    }

    void Show_ResultScreen()
    {
        GameScreen.SetActive(false);
        ResultScreen.SetActive(true);
        
        Invoke("BackToLobby", fResultScreenTime);
    }

    void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

}
