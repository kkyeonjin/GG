using System.Collections.Generic;
using UnityEngine;

public class SubwayItemMgr : MonoBehaviour
{
    public static SubwayItemMgr Instance = null;
    //아이템 스폰 위치 리스트

    //방해 아이템 사용 시 손에 생성되는 오브젝트 프리팹
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

    private void Awake()
    {
        var duplicated = FindObjectsOfType<SubwayItemMgr>();

        if (duplicated.Length > 1)
        {//이미 생성해서 플레이어 있음
            Destroy(this.gameObject);
        }
        else
        {//처음 생성
            if (null == Instance)
            {
                Instance = this;
            }
        }
    }
}