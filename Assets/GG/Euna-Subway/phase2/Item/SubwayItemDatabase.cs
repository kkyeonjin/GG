using System.Collections.Generic;
using UnityEngine;

public class SubwayItemDatabase : MonoBehaviour
{
    public static SubwayItemDatabase Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<SubwayItem> subItemDB = new List<SubwayItem>();
    [Space(20)]
    public GameObject subItemPrefab; //아이템 프리팹 -> 
    public Vector3[] subItemPos; //아이템 스폰 위치 관련

    private void Start()
    {
        //게임 실행 중 아이템 스폰
        GameObject _item = Instantiate(subItemPrefab, subItemPos[i], Quaternion.identity);
        
        // 아이템 종류 랜덤 결정
        //_item.GetComponent<SubwayItems>().SetItem(subItemDB[Random.Range(0,n)]);
    }
}