using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public void pickUp()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Pick up flashlight");
        GetComponent<SubwayItems>().ItemPick();

        Phase1Manager.clearCondition[1] = true;
    }
}