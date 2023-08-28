using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager : MonoBehaviour
{
    //게임 phase 관리
    int currentPhase; //현재 구간 숫자

    //phase별 랜덤 지진 이벤트 관리

    //

    /// <summary>
    /// - 플레이어 각자 개별씬에서 시작
    /// - 시작 7초 동안 지하철 덜컹이며 운행
    /// - 시작 7초 이후 지진 시작 + 재난 알림 문자음 사운드 + Order, HP 등 활성화
    /// (1) Holding Bar : 사운드 3초 이내에 hold 후 10초 동안 유지
    /// 
    /// - holding 종료 후 5초 동안 지진 잠잠해지면서 멈춤
    /// - 안내 방송 사운드 (열차에서 나와서 대피) but 문 안열림
    /// 
    /// (1.5) Emergency Call : 비상전화로 문 안열림 보고. 안내 sound
    /// 
    /// (2) 열차 문 비상 개방 : 비상 콕크 
    /// 
    /// </summary>
    /// 
    public enum ClearCondtion
    {
        lever, //비상핸들 돌리기
        cock, //비상콕크 돌리기
        collision, //npc와 충돌 
    }


    private void Awake()
    {

    }

    public GameObject B2;

    private void Start()
    {
        //B2.GetComponent<subwayRunning>();
    }

    private void Update()
    {

    }

    
}