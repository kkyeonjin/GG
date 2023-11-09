using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public int triggerNum;
    public GameObject hospital, police;
    public int time;
    GameObject building;
    public float timeSpent;

    //Vector3 rot;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(("Player")))
        {
            if (triggerNum == 1)
            {
                Debug.Log("Trigger1Enter");
                Shake_Phase3.instance.FIrstShake();
                HospitalFall();
            }
            else if (triggerNum == 2)
            {
                Debug.Log("Trigger2Enter");
                Shake_Phase3.instance.FIrstShake();
                PoliceFall();
            }
            //Shake_Phase3.instance.FIrstShake();
        }
    }

    void HospitalFall()
    {
        building = hospital;
        StartCoroutine(BuildingFall());
    }

    void PoliceFall()
    {
        building = police;
        StartCoroutine(BuildingFall());
    }
        IEnumerator BuildingFall()
        {
            timeSpent = 0;
            while (time > timeSpent)
            {
                timeSpent += Time.deltaTime;
                building.transform.localEulerAngles = new Vector3(Mathf.Lerp(building.transform.localEulerAngles.x, 40 * (timeSpent / time), timeSpent), building.transform.localEulerAngles.y, building.transform.localEulerAngles.z);
                yield return null;
            }
            if (time <= timeSpent)
            {
                Debug.Log("buildingFell");
                //SingleGameMgr.Instance.m_LocalPlayer.GetComponent<CharacterStatus>().Set_Damage(SingleGameMgr.Instance.m_LocalPlayer.GetComponent<CharacterStatus>().Get_MaxHP());
                yield break;
            }

        }
    }