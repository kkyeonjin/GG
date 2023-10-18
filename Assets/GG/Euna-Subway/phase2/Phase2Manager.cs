using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    public static Phase2Manager Instance = null;

    public GameObject PlatformTrigger;


    //�°��� ���Ϲ� ����
    public GameObject fallingObjects;
    public List<Transform> fallingObject;
    public List<Vector3> fallingObjectPos;
    public List<Quaternion> fallingObjectRot;

    private void Awake()
    {
        Instance = this;

        //���Ϲ� �ʱ� transform ����
        foreach (Transform fallObj in fallingObjects.transform)
        {
            fallingObject.Add(fallObj);
            fallingObjectPos.Add(fallObj.position);
            fallingObjectRot.Add(fallObj.rotation);
        }
    }

}