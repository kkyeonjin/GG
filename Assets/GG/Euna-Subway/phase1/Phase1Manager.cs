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
    /// - 지진 발생 시 
    /// - 문 비상개폐 
    /// - 
    /// </summary>
    public enum ClearCondtion
    {
        lever, //비상핸들 돌리기
        cock, //비상콕크 돌리기
        collision, //npc와 충돌 
    }


    private void Awake()
    {

    }

    private void Update()
    {

    }

    
}