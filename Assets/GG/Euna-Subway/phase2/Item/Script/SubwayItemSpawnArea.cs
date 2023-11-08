using System.Collections.Generic;
using UnityEngine;

public class SubwayItemSpawnArea : MonoBehaviour
{
    private Dictionary<Transform, GameObject> spawnPoints = new Dictionary<Transform, GameObject>();
    public GameObject itemOnField; //필드 아이템 프리팹

    private void Awake()
    {
        Transform[] spawnPoint = GetComponentsInChildren<Transform>();
        foreach(Transform t in spawnPoint)
        {
            spawnPoints.Add(t);
        }
    }

    private void Start()
    {
        //spawnItem();
    }

    //아이템 랜덤 스폰
    private void spawnItem (int idx)
    {
        
    }

    //아이템 먹힌 자리 확인 후 랜덤 스폰 실행
    private void checkBlank()
    {

    }
}