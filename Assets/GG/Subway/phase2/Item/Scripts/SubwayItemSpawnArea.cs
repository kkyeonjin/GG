using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SubwayItemSpawnArea : MonoBehaviour
{
    public Transform[] spawnPoints;

    private void Awake()
    {
       spawnPoints = GetComponentsInChildren<Transform>();
    }

    public void Spawn_Itmes()
    {
        int n;
        for (int i = 1; i< spawnPoints.Length; i++)
        {
            n = UnityEngine.Random.Range(0, SubwayItemMgr.Instance.subItemList.Count);
            GameObject newItem = Instantiate(SubwayItemMgr.Instance.subItemList[n].gameObject);
            newItem.transform.position = spawnPoints[i].transform.position;
        }
    }

    private void Start()
    {
        Spawn_Itmes();
    }

    //¾ÆÀÌÅÛ ¸ÔÈù ÀÚ¸® Ã¤¿ì±â
    private void checkBlank()
    {

    }
}