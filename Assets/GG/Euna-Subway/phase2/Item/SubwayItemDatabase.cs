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
    public GameObject subItemPrefab; //������ ������ -> 
    public Vector3[] subItemPos; //������ ���� ��ġ ����

    private void Start()
    {
        //���� ���� �� ������ ����
        GameObject _item = Instantiate(subItemPrefab, subItemPos[i], Quaternion.identity);
        
        // ������ ���� ���� ����
        //_item.GetComponent<SubwayItems>().SetItem(subItemDB[Random.Range(0,n)]);
    }
}