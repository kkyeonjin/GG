using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    public GameObject PlatformTrigger;


    //�°��� ���Ϲ� ����
    public GameObject fallingObjects;
    public List<Transform> fallingObject;

    private void Awake()
    {
        foreach (Transform fallObj in fallingObjects.transform)
        {
            fallingObject.Add(fallObj);
        }
    }

}