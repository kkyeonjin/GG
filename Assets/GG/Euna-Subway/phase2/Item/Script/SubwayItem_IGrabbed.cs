using UnityEngine;

public class SubwayItem_IGrabbed : SubwayItem_I
{
    public GameObject Collider;
    public GameObject Icon;
    public ParticleSystem particle;

    private bool isThrown = false;

    private void Start()
    {
        particle.Stop();
    }

    public void Set_isThrown(bool thrown, Vector3 throwDir)
    {
        isThrown = thrown;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.AddForce(throwDir.normalized*20f, ForceMode.Impulse);
        Debug.Log("Throw Item");
        Debug.LogError("Item Speed: " + rb.velocity.magnitude);
        Invoke("Destroy_AfterTimer", 10f);
    }

    private void Destroy_AfterTimer()
    {
        Destroy(this.gameObject);
    }

    private void Stop_Effect()
    {
        particle.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.LogError("아이템 다시 돌아옴" + other.gameObject.tag);
        //플레이어 타격 시 
        if (isThrown) 
        {
            if (other.transform.CompareTag("OtherPlayer"))
            {
                Player collided= other.gameObject.GetComponent<Player>();
                if(!collided.Is_MyPlayer())
                {
                    targetPlayer = collided;
                    base.Item_effect();
                }

                Collider.SetActive(false);
                Icon.SetActive(false);
                particle.Play();
                Invoke("Stop_Effect", 0.2f);
            }
        }
    }
}