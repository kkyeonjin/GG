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
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
            if (null == Instance)
            {
                Instance = this;
            }
        }
        */
        Instance = this;
    }

    //Field item ������
    public List<SubwayItem> subItemList = new List<SubwayItem>();
    [Space(20)]

    //Field item ���� ��ġ ����Ʈ
    public List<Vector3[]> subItemSpawnAreas = new List<Vector3[]>();

    //Grabbed ���� ������ ������
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

    private void Update()
    {
        //���� ���� �� ������ ����
        //GameObject _item = Instantiate(subItemPrefab, subItemPos[i], Quaternion.identity);

        // ������ ���� ���� ����
        //_item.GetComponent<SubwayItems>().SetItem(subItemDB[Random.Range(0,n)]);
    }

}