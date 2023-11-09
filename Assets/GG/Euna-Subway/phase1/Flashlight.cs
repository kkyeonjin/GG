using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public void pickUp()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Pick up flashlight");
        GetComponent<SubwayItem>().Item_pick();

        Phase1Mgr.Instance.Check_Condition(Phase1Mgr.phase1CC.Flashlight);
    }
}