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

    public RewardUI rewardui;

    public GameObject GameOver;
    public GameObject Canvas;

    private bool m_bPlayerGoalIn = false;

    private bool m_bIsGameOver = false;
    private bool m_bExit = false;

    void Awake()
    {

        var duplicated = FindObjectsOfType<SingleGameMgr>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
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

    public void Player_GoalIn()//�̱� ���
    {
        Debug.Log("player GoalIn!");
        m_bPlayerGoalIn = true;
        Invoke("Show_ResultScreen", fCeremonyTime);

        Reward_Player();

    }
   
    //////////�ΰ��ӿ��� ���� �Լ���//././///////
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
        m_bIsGameOver = true;
        Invoke("BackToLobby", 5f);

    }
    public void ExitStage()
    {
        m_bExit = true;
    }
    public void Check_Destroy()
    {
        if(m_bIsGameOver || m_bExit)
        {
            Destroy(m_LocalPlayerObj);
            Destroy(Canvas);
            Destroy(this.gameObject);
        }
    }

    public void Reward_Player()//���� ������ �κ�� ���ư�
    {
        //����� ������ ���� Info�� ������Ʈ
        //���� �� ���⼭ �ָ� �ɵ�
        float money;
        float exp;

        if (m_fPassedTime <= 180f)
        {
            money = 50;
            exp = 100;
            InfoHandler.Instance.Set_Exp(100);
            InfoHandler.Instance.Set_Money(50);
        }
        else if(m_fPassedTime <= 240f)
        {
            money = 40;
            exp = 80;
            InfoHandler.Instance.Set_Exp(80);
            InfoHandler.Instance.Set_Money(40);
        }
        else if (m_fPassedTime <= 300f)
        {
            money = 30;
            exp = 70;
            InfoHandler.Instance.Set_Exp(70);
            InfoHandler.Instance.Set_Money(30);
        }
        else if (m_fPassedTime <= 360f)
        {
            money = 20;
            exp = 60;
            InfoHandler.Instance.Set_Exp(60);
            InfoHandler.Instance.Set_Money(20);

        }
        else
        {
            money = 10;
            exp = 50;
            InfoHandler.Instance.Set_Exp(50);
            InfoHandler.Instance.Set_Money(10);
        }

        InfoHandler.Instance.Save_Info();

        rewardui.Get_Reward(money, exp, m_fPassedTime);
        
    }

    public void Cursor_Locked()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
