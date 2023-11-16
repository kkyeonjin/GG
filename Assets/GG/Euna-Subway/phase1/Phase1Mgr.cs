using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Phase1Mgr : MonoBehaviour
{
    public enum phase1CC
    {
        HoldBar,
        Flashlight,
        Lever
    }
    public float quakeStartTime = 10f;
    public float quakeStopTime = 30f;

    public static Phase1Mgr m_Instance = null;
    public bool[] clearCondition = new bool[3] { false, false, false }; //오더게이지 0 이상 , 비상 손전등 , 비상 레버

    //기둥 관련
    public bool playerIsHoldingBar = false;
    public List<HoldingBar> holdingBars;
    private bool holdingbarCleared = false;

    public GameObject Train1;
    public GameObject Train2;

    //지진
    public Earthquake earthquake;
    public CameraShake camearShake;

    //대응 수칙
    public GameObject PopUpScreen;
    public List<GameObject> PopUps = new List<GameObject>();
    public GameObject currentPopup;

    //사운드
    public AudioSource subwayNoise;
    public AudioSource emergencyAlarm;
    public AudioSource earthquakeNoise;

    //조명
    public List<GameObject> TrainLights;

    public PhotonView m_PV;
    private bool m_bNextPhase = true;



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


    private void Start()
    {
        //게임 시작 3초 뒤 지하철 출발
        //StartCoroutine("runTrain");

        //10초 뒤 지진 시작
        StartCoroutine("generateQuake");

        //30초 뒤 운행 중지 (자동)
        StartCoroutine("stopQuake");

        camearShake.earthquake = earthquake;
        subwayNoise.Play();
    }

    private void Update()
    {
        if (earthquake.isQuake)
        {
            Check_Column();
        }
    }

    public void Check_Column()
    {
        if (!playerIsHoldingBar)
        {
            SubwayInventory.instance.orderGage.Cut_Order();
        }
        else if (!holdingbarCleared)
        {
            holdingbarCleared = true;
            Check_Clear(phase1CC.HoldBar);
            //마이룸 수칙 해금
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            //UI 이펙트
            PopUp(PopUps[0]);
            Debug.Log("Column 해금");
        }
        else return;

    }

    IEnumerator generateQuake() {
        yield return new WaitForSeconds(quakeStartTime);

        //재난 문자 알림음
        earthquake.isQuake = true;
        camearShake.shakeCamera();        
        earthquakeNoise.Play();
        emergencyAlarm.Play();
        //B2.GetComponent<Earthquake>().t1 = Train1.transform;
        //B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        yield return new WaitForSeconds(quakeStopTime);
        earthquake.isQuake = false;
        earthquake.isQuakeStop = true;
        camearShake.shakeCameraStop();
        earthquakeNoise.Stop();
        subwayNoise.Stop();
        BlackOut();
        Debug.Log("Quake stop");
        //Check_Column();

        InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.TRAINSTOP);


        PopUp(PopUps[1]);
    }

    public void PopUp(GameObject popup)
    {
        currentPopup = popup;
        popup.SetActive(true);
        Invoke("dePopUp", 3f);
    }

    public void dePopUp() 
    {
        currentPopup.SetActive(false);
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
        //for(int i =0;i<4;i++) 
        //{
        //    PopUps.Add(PopUpScreen.transform.GetChild(i).gameObject);        
        //}
    }

    public void Check_Clear(phase1CC cleared)
    {
        switch (cleared)
        {
            case phase1CC.HoldBar:
                clearCondition[0] = true;
                break;
            case phase1CC.Flashlight:
                clearCondition[1] = true;
                break;
            case phase1CC.Lever:
                clearCondition[2] = true;
                break;
            default:
                break;

        }

        if (clearCondition[0] && clearCondition[1] && clearCondition[2])
        {
            if (m_bNextPhase)
            {
                Invoke("Change_Phase", 3f);
                m_bNextPhase = false;
            }
            Debug.Log("Clear!");
        }
    }

    void Change_Phase()
    {
        m_PV.RPC("Start_NextPhase", RpcTarget.All);
    }

    private void BlackOut()
    {
        foreach (GameObject light in TrainLights)
        {
            light.gameObject.SetActive(false);
        }
    }


    [PunRPC]
    void Start_NextPhase()
    {
        NetworkManager.Instance.StartGame("Multi_Subway_Phase2");
    }
}