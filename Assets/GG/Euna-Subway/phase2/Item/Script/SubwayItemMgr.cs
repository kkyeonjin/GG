using System.Collections.Generic;
using UnityEngine;

public class SubwayItemMgr : MonoBehaviour
{
    public static SubwayItemMgr m_Instance = null;
    private void Awake()
    {
        //�Ŵ��� �̱��� ����
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

    //�ʵ� ������ ������
    public GameObject itemOnField; 

    //Field item ������
    public List<SubwayItem> subItemList = new List<SubwayItem>();
    [Space(20)]

    //Field item ���� ��ġ ����Ʈ
    public List<Vector3[]> subItemSpawnAreas = new List<Vector3[]>();

    //Grabbed ���� ������ ������
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

}