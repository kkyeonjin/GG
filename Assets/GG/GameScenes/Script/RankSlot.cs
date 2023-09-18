using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RankSlot : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Time;
    public TextMeshProUGUI Rank;

    private Vector3 m_DestPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get_SlotInfo(string name,string time,int rank)
    {
        Name.text = name;
        Time.text = time;
        Rank.text = ""+rank;
    }
    public void ClearSlot()
    {
        Name.text = "";
        Time.text = "";
    }

    public void Set_Position(Vector3 destpos)
    {
        m_DestPos = destpos;
    }
}
