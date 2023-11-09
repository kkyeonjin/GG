using System.Collections.Generic;
using UnityEngine;

public class SubwayItemMgr : MonoBehaviour
{
    public static SubwayItemMgr m_Instance = null;
    private void Awake()
    {
        //매니저 싱글톤 생성
        var duplicated = FindObjectsOfType<SubwayItemMgr>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }
    }
    
    public static SubwayItemMgr Instance
    {
        get
        {
            if(null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }

    //필드 아이템 프리팹
    public GameObject itemOnField; 

    //Field item 프리팹
    public List<SubwayItem> subItemList = new List<SubwayItem>();
    [Space(20)]

    //Field item 스폰 위치 리스트
    public List<Vector3[]> subItemSpawnAreas = new List<Vector3[]>();

    //Grabbed 방해 아이템 프리팹
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

}