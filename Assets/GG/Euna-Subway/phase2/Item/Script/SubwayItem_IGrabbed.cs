using UnityEngine;

public class SubwayItem_IGrabbed : SubwayItem_I
{
    private bool isThrown = false;

    public void Set_isThrown(bool thrown)
    {
        isThrown = thrown;
    }
    protected override void Start()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        //�÷��̾� Ÿ�� �� 
        if (isThrown) 
        { 
            if (other.transform.CompareTag("player"))
            {
                targetPlayer = other.gameObject.GetComponent<Player>();

            }
            base.Item_effect();
            Destroy(this.gameObject);
        }
    }
}