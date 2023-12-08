using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Mgr : MonoBehaviour
{
    public static Phase2Mgr Instance = null;

    public GameObject PlatformTrigger;

    public bool resetFallings = false;

    //승강장 낙하물 관리
    public GameObject fallingObjects;
    public List<Transform> fallingObject;
    public List<Vector3> fallingObjectPos;
    public List<Quaternion> fallingObjectRot;

    public Earthquake earthquake;

    private void Awake()
    {
        Instance = this;

        //낙하물 초기 transform 저장
        foreach (Transform fallObj in fallingObjects.transform)
        {
            fallingObject.Add(fallObj);
            fallingObjectPos.Add(fallObj.position);
            fallingObjectRot.Add(fallObj.rotation);
        }

        
    }
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResetMap()
    {
        for (int i = 0; i < fallingObject.Count; i++)
        {
            fallingObject[i].position = fallingObjectPos[i];
            fallingObject[i].rotation = fallingObjectRot[i];
        }
        resetFallings = false;
    }


}