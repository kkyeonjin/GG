using UnityEngine;

public class SubwayItem_Grabbed : SubwayItem
{
    protected override void Start()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //�÷��̾� Ÿ�� �� 
        if (other.transform.CompareTag("Player"))
        {
            Item_effect();
        }

        ///�Ҹ�
        Destroy(this.gameObject);
    }

}