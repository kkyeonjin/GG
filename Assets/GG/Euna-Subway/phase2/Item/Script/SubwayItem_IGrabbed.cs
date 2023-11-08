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
            if (other.transform.CompareTag("Player"))
            {
                Player collided= other.gameObject.GetComponent<Player>();
                if(!collided.Is_MyPlayer())
                {
                    targetPlayer = collided;
                }
            }
            base.Item_effect();
            Destroy(this.gameObject);
        }
    }
}