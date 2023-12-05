using System.Collections.Generic;
using UnityEngine;

public class SubwayArea : MonoBehaviour
{
    ///<summary>
    /// - StartingPoint
    /// - CheckPoints
    /// - Bunkers
    /// - Falls
    /// - Obstacles
    /// - Items
    /// </summary>

    // 게임 시작점 구역
    public GameObject startingArea;
    BoxCollider startingAreaCollider; //Awake()에서 지정

    // 체크포인트 리스트
    //public List<Checkpoint> checkpoints;

    // 벙커 리스트
    // public List<GameObject> bunkers;

    // 아이템 리스트
    //private List<GameObject> items;
    //private Dictionary<Collider, item>;

    // 맵 내

    // 맵 내 

    /// <summary>
    /// - ResetMap() : 낙하물, 아이템 등 위치 리셋
    /// - 
    /// 
    /// </summary>
    ///

    private void Awake()
    {
        startingAreaCollider = startingArea.GetComponent<BoxCollider>();
    }

    public Vector3 returnRandomStartingPosition()
    {
        Vector3 originalPosition = startingArea.transform.position;

        float rangeX = startingAreaCollider.bounds.size.x;
        float rangeZ = startingAreaCollider.bounds.size.z;

        rangeX = Random.Range( (rangeX/2) * -1, rangeX/2 );
        rangeZ = Random.Range( (rangeZ/2) * -1, rangeZ/2 );
        Vector3 RandomPosition = new Vector3(rangeX, 0f, rangeZ);

        Vector3 respawnPosition = originalPosition + RandomPosition;
        return respawnPosition;
    }

    ///<summary>
    /// 맵 내 낙하물, 장애물, 아이템 위치 초기화
    /// </summary>
    public void ResetMap()
    {
        
    }
}
