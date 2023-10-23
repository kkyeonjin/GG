using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Phase1Mgr : MonoBehaviour
{
    public static Phase1Mgr m_Instance = null;
    public bool[] clearCondition = new bool[3] { true, false, false }; //오더게이지 0 이상 , 비상 손전등 , 비상 레버

    //기둥 관련
    public bool isHoldingBar = false;
    public List<HoldingBar> holdingBars;

    public GameObject Train1;
    public GameObject Train2;

    public Earthquake earthquake;

    //대응 수칙
    public GameObject PopUpScreen;
    public List<GameObject> PopUps = new List<GameObject>();

    public PhotonView m_PV;

    //phase별 랜덤 지진 이벤트 관리

    /// <summary>
    /// - 시작 10초 동안 지하철 덜컹이며 운행
    /// - 시작 10초 이후 지진 시작 + 재난 알림 문자음 사운드 + Order, HP 등 활성화
    /// (1) Holding Bar : 사운드 3초 이내에 hold 후 10초 동안 유지
    /// 
    /// - holding 종료 후 5초 동안 지진 잠잠해지면서 멈춤
    /// - 안내 방송 사운드 (열차에서 나와서 대피) but 문 안열림
    /// 
    /// (2) 비상 손전등 픽업
    /// 
    /// (3) 열차 문 비상 개방 : 비상 레버 회전
    /// 
    /// </summary>

    private bool AllClear = false;


    private void Start()
    {
        //게임 시작 3초 뒤 지하철 출발
        //StartCoroutine("runTrain");

        //10초 뒤 지진 시작
        StartCoroutine("generateQuake");

        //30초 뒤 운행 중지 (자동)
        StartCoroutine("stopQuake");
    }

    private void Update()
    {

        if (InGameUIMgr.Instance.m_bGoalCountDown)
        {
            Debug.Log("Goal Count Down!");
        }
        if (clearCondition[0] && clearCondition[1] && clearCondition[2])
        {
            //게임 종료 후 대기 
            m_PV.RPC("Start_NextPhase", RpcTarget.All);
            Debug.Log("Clear!");
        }
    }

    void Check_Column()
    {
        if (SubwayInventory.instance.orderGage.Get_Order() > 0f)
        {
            Debug.Log("Column 해금");
            //마이룸 수칙 해금
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            //UI 이펙트
            PopUp(PopUps[0]);
        }
        else
        {
            //Game Over
            //GameMgr.Instance.Game_Over();
            Debug.Log("Order Gage run out");
        }
    }

    public GameObject B2;

    IEnumerator runTrain()
    {
        yield return new WaitForSeconds(3.5f);
    }

    IEnumerator generateQuake() {
        yield return new WaitForSeconds(10f);

        //재난 문자 알림음
        earthquake.isQuake = true;
        Debug.Log("isQuake" + earthquake.isQuake);
        B2.GetComponent<Earthquake>().t1 = Train1.transform;
        B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        yield return new WaitForSeconds(15f);
        earthquake.isQuake = false;
        earthquake.isQuakeStop = true;
        Check_Column();
    }

    IEnumerator PopUp(GameObject popup)
    {
        popup.SetActive(true);
        yield return new WaitForSeconds(3f);
        popup.SetActive(false);
    }



    //싱글톤 
    public static Phase1Mgr Instance
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
    private void Awake()
    {
        var duplicated = FindObjectsOfType<GameMgr>();

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

        //Popups 받아오기
        for(int i =0;i<4;i++) 
        {
            PopUps.Add(PopUpScreen.transform.GetChild(i).gameObject);        
        }
    }

    [PunRPC]
    void Start_NextPhase()
    {
        NetworkManager.Instance.StartGame("Multi_Subway_Phase2");
    }
}