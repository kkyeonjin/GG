using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1Manager : MonoBehaviour
{
    //���� phase ����
    int currentPhase; //���� ���� ����

    //phase�� ���� ���� �̺�Ʈ ����

    //

    /// <summary>
    /// - �÷��̾� ���� ���������� ����
    /// - ���� 7�� ���� ����ö �����̸� ����
    /// - ���� 7�� ���� ���� ���� + �糭 �˸� ������ ���� + Order, HP �� Ȱ��ȭ
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
        lever, //����ڵ� ������
        cock, //�����ũ ������
        collision, //npc�� �浹 
    }


    private void Awake()
    {

    }

    public GameObject B2;

    private void Start()
    {
        //B2.GetComponent<subwayRunning>();
    }

    private void Update()
    {

    }

    
}