using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Phase1Mgr : MonoBehaviour
{
    public enum phase1CC
    {
        HoldBar,
        Flashlight,
        Lever
    }
    public float quakeStartTime = 10f;
    public float quakeStopTime = 30f;

    public static Phase1Mgr m_Instance = null;
    public bool[] clearCondition = new bool[3] { false, false, false }; //���������� 0 �̻� , ��� ������ , ��� ����

    //��� ����
    public bool playerIsHoldingBar = false;
    public List<HoldingBar> holdingBars;
    private bool holdingbarCleared = false;

    public GameObject Train1;
    public GameObject Train2;

    //����
    public Earthquake earthquake;
    public CameraShake camearShake;

    //���� ��Ģ
    public GameObject PopUpScreen;
    public List<GameObject> PopUps = new List<GameObject>();
    public GameObject currentPopup;

    //����
    public AudioSource subwayNoise;
    public AudioSource emergencyAlarm;
    public AudioSource earthquakeNoise;

    //����
    public List<GameObject> TrainLights;

    public PhotonView m_PV;
    private bool m_bNextPhase = true;



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


    private void Start()
    {
        //���� ���� 3�� �� ����ö ���
        //StartCoroutine("runTrain");

        //10�� �� ���� ����
        StartCoroutine("generateQuake");

        //30�� �� ���� ���� (�ڵ�)
        StartCoroutine("stopQuake");

        camearShake.earthquake = earthquake;
        subwayNoise.Play();
    }

    private void Update()
    {
        if (earthquake.isQuake)
        {
            Check_Column();
        }
    }

    public void Check_Column()
    {
        if (!playerIsHoldingBar)
        {
            SubwayInventory.instance.orderGage.Cut_Order();
        }
        else if (!holdingbarCleared)
        {
            holdingbarCleared = true;
            Check_Clear(phase1CC.HoldBar);
            //���̷� ��Ģ �ر�
            InfoHandler.Instance.Unlock_Manual(InfoHandler.SUBWAY.COLUMN);
            //UI ����Ʈ
            PopUp(PopUps[0]);
            Debug.Log("Column �ر�");
        }
        else return;

    }

    IEnumerator generateQuake() {
        yield return new WaitForSeconds(quakeStartTime);

        //�糭 ���� �˸���
        earthquake.isQuake = true;
        camearShake.shakeCamera();        
        earthquakeNoise.Play();
        emergencyAlarm.Play();
        //B2.GetComponent<Earthquake>().t1 = Train1.transform;
        //B2.GetComponent<Earthquake>().t2 = Train2.transform;
    }

    IEnumerator stopQuake()
    {
        yield return new WaitForSeconds(quakeStopTime);
        earthquake.isQuake = false;
        earthquake.isQuakeStop = true;
        camearShake.shakeCameraStop();
        earthquakeNoise.Stop();
        subwayNoise.Stop();
        BlackOut();
        Debug.Log("Quake stop");
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
        //for(int i =0;i<4;i++) 
        //{
        //    PopUps.Add(PopUpScreen.transform.GetChild(i).gameObject);        
        //}
    }

    public void Check_Clear(phase1CC cleared)
    {
        switch (cleared)
        {
            case phase1CC.HoldBar:
                clearCondition[0] = true;
                break;
            case phase1CC.Flashlight:
                clearCondition[1] = true;
                break;
            case phase1CC.Lever:
                clearCondition[2] = true;
                break;
            default:
                break;

        }

        if (clearCondition[0] && clearCondition[1] && clearCondition[2])
        {
            if (m_bNextPhase)
            {
                Invoke("Change_Phase", 3f);
                m_bNextPhase = false;
            }
            Debug.Log("Clear!");
        }
    }

    void Change_Phase()
    {
        m_PV.RPC("Start_NextPhase", RpcTarget.All);
    }

    private void BlackOut()
    {
        foreach (GameObject light in TrainLights)
        {
            light.gameObject.SetActive(false);
        }
    }


    [PunRPC]
    void Start_NextPhase()
    {
        NetworkManager.Instance.StartGame("Multi_Subway_Phase2");
    }
}