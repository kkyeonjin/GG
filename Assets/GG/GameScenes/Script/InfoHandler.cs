using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class InfoHandler : MonoBehaviour
{
    private PlayerInfo m_Playerinfo;
    private static InfoHandler m_Instance = null;

    void Awake()
    {
        var duplicated = FindObjectsOfType<InfoHandler>();

        if (duplicated.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else if (null == m_Instance)
        {
            m_Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public static InfoHandler Instance
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

    // Start is called before the first frame update
    void Start()
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.streamingAssetsPath, "PlayerInfo"), FileMode.Open, FileAccess.Read);

        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);

        string jsondata = Encoding.UTF8.GetString(data);
        Debug.Log(jsondata);
        fileStream.Close();

        m_Playerinfo = JsonConvert.DeserializeObject<PlayerInfo>(jsondata);

        Debug.Log(m_Playerinfo.Get_Level());

    }
    public int Get_Level()
    {
        return m_Playerinfo.Get_Level();
    }

    public int Get_Exp()
    {
        return m_Playerinfo.Get_Exp();
    }

    public int Get_ExpMax()
    {
        return m_Playerinfo.Get_ExpMax();
    }

    public int Get_Money()
    {
        return m_Playerinfo.Get_Money();
    }
    public int Get_CurrCharacter()
    {
        return m_Playerinfo.Get_CurrCharacter();
    }
    public void Set_Exp(int iExp)
    {
        m_Playerinfo.Set_Exp(iExp);
    }
    public void Set_Money(int iMoney)
    {
        m_Playerinfo.Set_Money(iMoney);
    }

    public void Set_CurrCharacter(int iCurrIndex)
    {
        m_Playerinfo.Set_CurrCharacter(iCurrIndex);
        Save_Info();
    }

    public bool Is_Character_Available(int iIndex)
    {
        return m_Playerinfo.Is_Character_Available(iIndex);
    }

    public void Set_Character_Available(int iIndex)
    {
        m_Playerinfo.Set_Character_Available(iIndex);
    }

    public void Save_Info()
    {
        //json 파일 부분만 수정하는 방법이 뭐냐 대체
        //json생성(FileMode.Create) & 저장(FileAccess.Write)
        //FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", Application.dataPath, "PlayerInfo"), FileMode.Create, FileAccess.Write);
        string jsondata = JsonConvert.SerializeObject(m_Playerinfo);
        Debug.Log(jsondata);
        byte[] data = Encoding.UTF8.GetBytes(jsondata);
    
        File.WriteAllText(Application.dataPath + "/PlayerInfo.json",jsondata);

        //fileStream.Write(data, 0, data.Length);
        //fileStream.Close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
