using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager : MonoBehaviour
{
    //ī�޶� ��ȯ
    public Camera initCam;
    public Camera playerCam;

    public GameObject Train1;
    public GameObject Train2;

    //phase�� ���� ���� �̺�Ʈ ����

    /// <summary>
    /// - ���� 10�� ���� ����ö �����̸� ����
    /// - ���� 10�� ���� ���� ���� + �糭 �˸� ������ ���� + Order, HP �� Ȱ��ȭ
    /// (1) Holding Bar : ���� 3�� �̳��� hold �� 10�� ���� ����
    /// 
    /// - holding ���� �� 5�� ���� ���� ���������鼭 ����
    /// - �ȳ� ��� ���� (�������� ���ͼ� ����) but �� �ȿ���
    /// 
    /// (1.5) Emergency Call : �����ȭ�� �� �ȿ��� ����. �ȳ� sound
    /// 
    /// (2) ���� �� ��� ���� : ��� ��ũ 
    /// 
    /// </summary>
    /// 
    public enum ClearCondtion
    {
        call,
        lever, //����ڵ� ������
        collision, //npc�� �浹 
    }
    private bool AllClear = false;

    private void Awake()
    {
    }



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
        Earthquake.isQuake = true;
        B2.GetComponent<Earthquake>().t1 = Train1.transform;
        B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        yield return new WaitForSeconds(15f);
        Earthquake.isQuake = false;
    }
}