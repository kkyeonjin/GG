using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerInfo
{
    public string Name;

    public int Level;
    public int Exp;
    public int ExpMax;
    public int Money;

    public int CurrCharacter;
    public bool[] AvailableCharacter;

    public int[] ItemsInfo;

    public PlayerInfo()
    {

        //Level = 1;
        //Exp = 0;
        //ExpMax = 150;
        //Money = 0;

        //CurrCharacter = 0;
        //AvailableCharacter = new bool[(int)Player.CHARACTER.END];
        //AvailableCharacter[0] = true;
        //for (int i = 1; i < (int)Player.CHARACTER.END; ++i)
        //    AvailableCharacter[i] = false;

        //ItemsInfo = new int[(int)Item.ITEM.END];
        //for (int i = 0; i < (int)Item.ITEM.END; ++i)
        //    ItemsInfo[i] = 0;

        //HoldingItems = new int[2, 2];
        //for (int i = 0; i < 2; ++i)
        //    for (int j = 0; j < 2; ++j)
        //        HoldingItems[i,j] = -1;

    }

    void Start()
    {

    }
    public string Get_Name()
    {
        return Name;
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
    public void Set_Name(string name)
    {
        Name = name;
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

    public void Buy_Item(int iIndex, int iNum)
    {
        ItemsInfo[iIndex] += iNum;
    }

    public int Get_Item_Num(int iIndex)
    {
        return ItemsInfo[iIndex];
    }

}
