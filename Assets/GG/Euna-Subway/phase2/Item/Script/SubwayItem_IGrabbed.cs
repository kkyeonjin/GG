using UnityEngine;

public class SubwayItem_IGrabbed : SubwayItem_I
{
    private bool isThrown = false;

    public void Set_isThrown(bool thrown, Vector3 throwDir)
    {
        isThrown = thrown;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.AddForce(throwDir.normalized*10f, ForceMode.Impulse);
        Debug.Log("Throw Item");
        Debug.LogError("Item Speed: " + rb.velocity.magnitude);
    }

    private void OnCollisionEnter(Collision other)
    {
        //플레이어 타격 시 
        if (isThrown) 
        {
            if (other.transform.CompareTag("Player"))
            {
                Player collided= other.gameObject.GetComponent<Player>();
                if(!collided.Is_MyPlayer())
                {
                    targetPlayer = collided; 
                }
            base.Item_effect();
            Destroy(this.gameObject);
            }
        }
    }
}