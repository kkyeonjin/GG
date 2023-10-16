using System.Collections.Generic;
using UnityEngine;

public class SubwayItemMgr : MonoBehaviour
{
    public static SubwayItemMgr Instance = null;
    //������ ���� ��ġ ����Ʈ

    //���� ������ ��� �� �տ� �����Ǵ� ������Ʈ ������
    public List<GameObject> GrabbableItems = new List<GameObject>(new GameObject[3]);

    private void Awake()
    {
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
    }
}