using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameMgr m_Instance = null;

    public Player m_LocalPlayer;
    public GameObject m_LocalPlayerObj;
    public string m_szPlayerPrefab = "Local_Player";

    public ItemSelectUI[] m_HoldingItemUI;

    private int[,] m_HoldingItem;

    void Awake()
    {
        Debug.LogWarning("호출");
        PhotonNetwork.Instantiate(m_szPlayerPrefab, Vector3.zero, Quaternion.identity);

        var duplicated = FindObjectsOfType<GameMgr>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else if (null == m_Instance)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        m_HoldingItem = new int[2,2];
        m_HoldingItem[0, 0] = -1;
        m_HoldingItem[1, 0] = -1;
        m_HoldingItem[0, 1] = -1;
        m_HoldingItem[1, 1] = -1;

    }

    public static GameMgr Instance
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

    public bool Set_HoldingItem(ItemSelectUI iInput)
    {
        for(int i=0;i<2;++i)
        {   
            if(m_HoldingItem[i,0] == -1)
            {
                m_HoldingItem[i, 0] = iInput.Get_Index();
                m_HoldingItemUI[i].Set_Image(iInput.Get_Image());
                m_HoldingItemUI[i].Have_Items(true);
                //개수 설정
                return true;
            }
        }
        return false;//빈자리 없음
    }
    public void Set_Unholding(int iIndex)
    {
        m_HoldingItem[iIndex, 0] = -1;
        m_HoldingItem[iIndex, 1] = -1;
    }
    public int[,] Get_HoldingItem()
    {
        return m_HoldingItem;
    }
    public void Set_PlayerPos()
    {
        if (null != m_LocalPlayerObj)
            m_LocalPlayerObj.transform.position = Vector3.zero;
    }
    public void Destroy_Player()
    {
        Destroy(m_LocalPlayerObj);
    }

    public void Destroy_Myself()
    {
        Destroy(this.gameObject);
    }
    public void Set_Camera()
    {

        CinemachineVirtualCamera Temp = FindObjectOfType<CinemachineVirtualCamera>();
        if(null!=Temp && null != m_LocalPlayer)
            m_LocalPlayer.Set_Camera(Temp);        
    }

    public void Change_Avatar(int iIndex)
    {
        m_LocalPlayer.GetComponentInChildren<ChangeAvatar>().Change_Avatar(iIndex);
    }

    public void OnPhotonSerializeView(PhotonStream photonstream, PhotonMessageInfo photonmessageinfo)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
