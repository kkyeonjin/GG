using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SubwayItemSpawnArea : MonoBehaviour
{
    private spawnPoint[] spawnPoints;
    public GameObject itemOnField; //�ʵ� ������ ������

    private void Awake()
    {
        Transform[] spawnPoint = GetComponentsInChildren<Transform>();

    }

    private void Start()
    {
        //spawnItem();
    }

    //������ ���� ����
    private void spawnItem (int idx)
    {
        
    }

    //������ ���� �ڸ� Ȯ�� �� ���� ���� ����
    private void checkBlank()
    {

    }
}

public class spawnPoint
{
    public Transform[] t;
}