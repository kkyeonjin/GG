using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Phase1Mgr : MonoBehaviour
{
    public float quakeStartTime = 6f;
    public float quakeStopTime = 20f;

    public static Phase1Mgr m_Instance = null;
    public bool[] clearCondition = new bool[3] { true, false, false }; //���������� 0 �̻� , ��� ������ , ��� ����

    //��� ����
    public bool isHoldingBar = false;
    public List<HoldingBar> holdingBars;

    public GameObject Train1;
    public GameObject Train2;

    //����
    public Earthquake earthquake;

    //���� ��Ģ
    public GameObject PopUpScreen;
    public List<GameObject> PopUps = new List<GameObject>();
    public GameObject currentPopup;

    public PhotonView m_PV;
    private bool m_bNextPhase = true;

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

    private bool AllClear = false;


    private void Start()
    {
        //���� ���� 3�� �� ����ö ���
        //StartCoroutine("runTrain");

        //10�� �� ���� ����
        StartCoroutine("generateQuake");

        //30�� �� ���� ���� (�ڵ�)
        StartCoroutine("stopQuake");
    }

    private void Update()
    {
        if (InGameUIMgr.Instance.m_bGoalCountDown)
        {
            Debug.Log("Goal Count Down!");
        }
        if (clearCondition[0] && clearCondition[1] && clearCondition[2])
        {
            //���� ���� �� ��� 
            
            if (m_bNextPhase)
            {
                m_PV.RPC("Start_NextPhase", RpcTarget.All);
                m_bNextPhase = false;
            }
            Debug.Log("Clear!");
        }
        Debug.Log("clear 0 1 2 : " + clearCondition[0] + clearCondition[1] + clearCondition[2]);
    }

    public void Check_Column()
    {
        //if (SubwayInventory.instance.orderGage.Get_Order() > 0f)
        //{
            Debug.Log("Column �ر�");
            //���̷� ��Ģ �ر�
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            //UI ����Ʈ
            PopUp(PopUps[0]);
        //}
        //else
        //{
            //Game Over
            //GameMgr.Instance.Game_Over();
            //Debug.Log("Order Gage run out");
        //}
    }

    public GameObject B2;

    IEnumerator runTrain()
    {
        yield return new WaitForSeconds(3.5f);
    }

    IEnumerator generateQuake() {
        yield return new WaitForSeconds(quakeStartTime);

        //�糭 ���� �˸���
        earthquake.isQuake = true;
        Debug.Log("isQuake" + earthquake.isQuake);
        //B2.GetComponent<Earthquake>().t1 = Train1.transform;
        //B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        Debug.Log("Quake stop");
        yield return new WaitForSeconds(quakeStopTime);
        earthquake.isQuake = false;
        earthquake.isQuakeStop = true;
        //Check_Column();

        InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.TRAINSTOP);


        PopUp(PopUps[1]);
    }

    public void PopUp(GameObject popup)
    {
        currentPopup = popup;
        popup.SetActive(true);
        Invoke("dePopUp", 3f);
    }

    public void dePopUp() 
    {
        currentPopup.SetActive(false);
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

        //Popups �޾ƿ���
        for(int i =0;i<4;i++) 
        {
            PopUps.Add(PopUpScreen.transform.GetChild(i).gameObject);        
        }
    }

    [PunRPC]
    void Start_NextPhase()
    {
        NetworkManager.Instance.StartGame("Multi_Subway_Phase2");
    }
}