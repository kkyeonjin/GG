using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public void pickUp()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Pick up flashlight");
        GetComponent<SubwayItem>().Item_pick();

        Phase1Mgr.Instance.clearCondition[1] = true;
    }
}