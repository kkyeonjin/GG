using System.Collections.Generic;
using UnityEngine;

public class SubwayItemMgr : MonoBehaviour
{
    public static SubwayItemMgr Instance = null;
    private void Awake()
    {
        /*
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
        */
        Instance = this;
    }

    //Field item 프리팹
    public List<SubwayItem> subItemList = new List<SubwayItem>();
    [Space(20)]

    //Field item 스폰 위치 리스트
    public List<Vector3[]> subItemSpawnAreas = new List<Vector3[]>();

    //Grabbed 방해 아이템 프리팹
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

    private void Update()
    {
        //게임 실행 중 아이템 스폰
        //GameObject _item = Instantiate(subItemPrefab, subItemPos[i], Quaternion.identity);

        // 아이템 종류 랜덤 결정
        //_item.GetComponent<SubwayItems>().SetItem(subItemDB[Random.Range(0,n)]);
    }

}