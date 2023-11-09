using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class PuzzleMgr : MonoBehaviour
{
    public static PuzzleMgr instance;
    public int[] passedPuzzle = new int[3] { 1, 1, 1 };
    public bool[] valvePuzzle = new bool[2] { false, false };
    public bool[] activeCam = new bool[4] { false, false, false, false };
    public int playingPhase;
    public bool inElevator;
    public GameObject light1, light2;
    public GameObject book;
    public Material[] mat = new Material[2];
    public GameObject manual1, manual2, manual3;

    public GameObject[] ManualImg;
    private GameObject CurManualImg;

    public float time;
    private float curTime;
    public TMP_Text timeText;
    private float minute, second;
    public GameObject Timer;

    public GameObject Player;

    public List<GameObject> objs2Off = new List<GameObject>(new GameObject[8]);
    public List<GameObject> objs2On = new List<GameObject>(new GameObject[2]);

    public GameObject obstacle;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;

        //DontDestroyOnLoad(this);

        activeCam[0] = true;
        playingPhase = 1;
    }

    private void Update()
    {
       // Test();
    }

    public void Manual1Unlock()
    {
        InfoHandler.Instance.Unlock_Manual(InfoHandler.HOUSE.GAS);
        manual1.SetActive(true);
        Invoke("Manual1Close", 2f);

        Active_ManualUI((int)InfoHandler.HOUSE.GAS);
    }

    public void Manual1Close()
    {
        manual1.SetActive(false);
    }

    public void MakeObstacle()
    {
        obstacle.gameObject.transform.localPosition = new Vector3(Player.transform.localPosition.x, Player.transform.localPosition.y + 7f, Player.transform.localPosition.z);
        obstacle.SetActive(true);
    }

    public void Test()
    {
        if(Input.GetKeyUp(KeyCode.V))
        {
            Shake.instance.EarthQuake();
        }
    }
    public void Manual2Unlock()
    {
        InfoHandler.Instance.Unlock_Manual(InfoHandler.HOUSE.PACKING);
        manual2.SetActive(true);
        Invoke("Manual2Close", 2f);

        Active_ManualUI((int)InfoHandler.HOUSE.PACKING);

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

        Active_ManualUI((int)InfoHandler.HOUSE.TABLE);

    }

    public void Manual3Open()
    {
        manual3.SetActive(true);
    }

    public void Manual3Close()
    {
        manual3.SetActive(false);
    }

    public void SwitchLight()
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
    void Active_ManualUI(int idx)
    {
        CurManualImg = ManualImg[idx];
        CurManualImg.SetActive(true);

        Invoke("DeAvtivate_ManualUI", 3f);
    }

    void DeAvtivate_ManualUI()
    {
        CurManualImg.SetActive(false);
    }

    public void TimerStart()
    {
        Timer.SetActive(true);
        StartCoroutine(TimerStartCouroutine());
    }

    public void Back2Main()
    {
        for (int i = 0; i < objs2Off.Count; i++)
        {
            objs2Off[i].SetActive(false);
        }
        objs2On[0].SetActive(true);
        objs2On[1].SetActive(true);
    }

    IEnumerator TimerStartCouroutine()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            timeText.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if (curTime <= 0)
            {
                Shake.instance.FIrstShake();
                //Shake.instance.EarthQuake();
                
                if (passedPuzzle[0] == 0 && passedPuzzle[1] == 0 && passedPuzzle[2] == 0)
                {
                    HidingPuzzle.instance.MsgPop();
                    //Shake.instance.EarthQuake();
                }
          
                Debug.Log("시간 종료");
                Timer.SetActive(false);
                if (activeCam[1] == true || activeCam[2] == true || activeCam[3] == true)
                {
                    
                }
                else
                {
                    Back2Main();
                }
                MakeObstacle();
                curTime = 0;
                playingPhase = 2;
                yield break;
            }
        }
    }
    private void OnDestroy()
    {
        Destroy(SingleGameMgr.Instance.m_LocalPlayerObj);
        Destroy(SingleGameMgr.Instance.Canvas);
        Destroy(SingleGameMgr.Instance.gameObject);
    }
}
