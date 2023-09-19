using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceState : MonoBehaviour
{
    public GameObject[] walls;// 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] eleDoors;

    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            //doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }

    public void DoorUpdate()
    {
        for(int i =0; i < walls.Length; i++)
        {
            if (walls[i].activeSelf == true)
            {
                eleDoors[i].SetActive(true);
                // walls[i].SetActive(false);
                break;
            }      
        }
    }
}