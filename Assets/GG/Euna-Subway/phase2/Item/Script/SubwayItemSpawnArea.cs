using System.Collections.Generic;
using UnityEngine;

public class SubwayItemSpawnArea : MonoBehaviour
{
    private Dictionary<Transform, GameObject> spawnPoints = new Dictionary<Transform, GameObject>();
    public GameObject itemOnField; //�ʵ� ������ ������

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

    //������ ���� ����
    private void spawnItem (int idx)
    {
        
    }

    //������ ���� �ڸ� Ȯ�� �� ���� ���� ����
    private void checkBlank()
    {

    }
}