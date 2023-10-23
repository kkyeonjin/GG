using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Mgr : MonoBehaviour
{
    public static Phase1Mgr m_Instance = null;
    public static bool[] clearCondition = new bool[3] { true, false, false }; //���������� 0 �̻� , ��� ������ , ��� ����

    public GameObject Train1;
    public GameObject Train2;

    public Earthquake earthquake;

    //phase�� ���� ���� �̺�Ʈ ����

    /// <summary>
    /// - ���� 10�� ���� ����ö �����̸� ����
    /// - ���� 10�� ���� ���� ���� + �糭 �˸� ������ ���� + Order, HP �� Ȱ��ȭ
    /// (1) Holding Bar : ���� 3�� �̳��� hold �� 10�� ���� ����
    /// 
    /// - holding ���� �� 5�� ���� ���� ���������鼭 ����
    /// - �ȳ� ��� ���� (�������� ���ͼ� ����) but �� �ȿ���
    /// 
    /// (2) ��� ������ �Ⱦ�
    /// 
    /// (3) ���� �� ��� ���� : ��� ���� ȸ��
    /// 
    /// </summary>

    public enum ClearCondtion
    {
        OrderGage,
        lever, //����ڵ� ������
        collision, //npc�� �浹 
    }

    private bool AllClear = false;



    private void Start()
    {
        //���� ���� 3�� �� ����ö ���


        //10�� �� ���� ����
        StartCoroutine("generateQuake");
        StartCoroutine("stopQuake");

        //30�� �� ���� ���� (�ڵ�)
    }

    private void Update()
    {
        if (AllClear)
        {
            //���� ���� �� ��� 

        }
    }

    public GameObject B2;
    IEnumerator generateQuake() {
        yield return new WaitForSeconds(10f);

        //�糭 ���� �˸���
        earthquake.isQuake = true;
        Debug.Log("isQuake" + earthquake.isQuake);
        B2.GetComponent<Earthquake>().t1 = Train1.transform;
        B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        yield return new WaitForSeconds(15f);
        earthquake.isQuake = false;
        earthquake.isQuakeStop = true;
        Check_Column();
    }

    void Check_Column()
    {
        if (SubwayInventory.instance. > 0f)
        {
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            Debug.Log("Column �ر�");
            //UI ����Ʈ
        }
        else
        {
            //Game Over
            GameMgr.Instance.Game_Over();
        }
    }


    //�̱��� 
    public static Phase1Mgr Instance
    {
        get
        {
            if (null == m_Instance)
            {
                return null;
            }
            return m_Instance;
        }
    }
    private void Awake()
    {
        var duplicated = FindObjectsOfType<GameMgr>();

        if (duplicated.Length > 1)
        {//�̹� �����ؼ� �÷��̾� ����
            Destroy(this.gameObject);
        }
        else
        {//ó�� ����
            if (null == m_Instance)
            {
                m_Instance = this;
            }
        }
    }
}