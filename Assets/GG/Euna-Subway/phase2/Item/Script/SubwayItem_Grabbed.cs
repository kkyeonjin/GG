using UnityEngine;

public class SubwayItem_Grabbed : SubwayItem
{
    protected override void Start()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //플레이어 타격 시 
        if (other.transform.CompareTag("Player"))
        {
            Item_effect();
        }

        ///소멸
        Destroy(this.gameObject);
    }

}