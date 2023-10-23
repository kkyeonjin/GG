using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Mgr : MonoBehaviour
{
    public static Phase1Mgr m_Instance = null;
    public static bool[] clearCondition = new bool[3] { true, false, false }; //오더게이지 0 이상 , 비상 손전등 , 비상 레버

    public GameObject Train1;
    public GameObject Train2;

    public Earthquake earthquake;

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

    public enum ClearCondtion
    {
        OrderGage,
        lever, //비상핸들 돌리기
        collision, //npc와 충돌 
    }

    private bool AllClear = false;



    private void Start()
    {
        //게임 시작 3초 뒤 지하철 출발


        //10초 뒤 지진 시작
        StartCoroutine("generateQuake");
        StartCoroutine("stopQuake");

        //30초 뒤 운행 중지 (자동)
    }

    private void Update()
    {
        if (AllClear)
        {
            //게임 종료 후 대기 

        }
    }

    public GameObject B2;
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

    void Check_Column()
    {
        if (SubwayInventory.instance. > 0f)
        {
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            Debug.Log("Column 해금");
            //UI 이펙트
        }
        else
        {
            //Game Over
            GameMgr.Instance.Game_Over();
        }
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
    }
}