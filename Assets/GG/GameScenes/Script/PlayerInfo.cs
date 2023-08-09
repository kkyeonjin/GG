using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerInfo
{
     public int Level;
     public int Exp;
     public int ExpMax;
     public int Money;
     
     public int CurrCharacter;
     public bool[] AvailableCharacter;

     public int[][] CurrItem;

    public PlayerInfo()
    {


    }


    public int Get_Level()
    {
        return Level;
    }

    public int Get_Exp()
    {
        return Exp;
    }

    public int Get_ExpMax()
    {
        return ExpMax;
    }

    public int Get_Money()
    {
        return Money;
    }
    public int Get_CurrCharacter()
    {
        return CurrCharacter;
    }
    public void Set_Exp(int iExp)
    {
        Exp += iExp;
    }
    public void Set_Money(int iMoney)
    {
        Money += iMoney;
    }

    public void Set_CurrCharacter(int iCurrIndex)
    {
        CurrCharacter = iCurrIndex;
    }

    public bool Is_Character_Available(int iIndex)
    {
        return AvailableCharacter[iIndex];
    }

    public void Set_Character_Available(int iIndex)
    {
        AvailableCharacter[iIndex] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
