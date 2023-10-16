using System.Collections;
using UnityEngine;

[System.Serializable]
public class SubwayItem : MonoBehaviour
{
    public enum ItemType
    {
        // �ڱ� ��ȭ
        ENFORCEMENT,
        /*
        HP, //HP ȸ��
        STAMINA, //���¹̳� ȸ�� (Max�� 30%p ����)
        ORDER, //���������� ȸ��
        */

        // ��� ����
        INTERRUPT
        /*,
        KNOCKDOWN, //�Ѿ�߸��� (Max HP�� 15%p ����)
        SLOWDOWN, //�޸��� �ӵ� �ݰ� 5��
        SLOWDOWNALL //(�õ��) ��ü �÷��̾� �޸��� �ӵ� �ݰ� 5��
        */
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public virtual void Use_Item()
    {
        // return false; //������ ��� ���� ����
    }

    public void Vanish()
    {
        StartCoroutine("vanishParticle");
        Destroy(this.gameObject.GetComponent<SphereCollider>());
        this.transform.Find("icon").gameObject.SetActive(false);
        this.transform.Find("Sphere").gameObject.SetActive(false);
    }

    IEnumerator vanishParticle()
    {
        yield return new WaitForSeconds(0.8f);
        this.transform.Find("Particle System").gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    //trigger sphere �ݰ濡 ���� ������ �ڵ� pick up
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponent<SubwayItems>().ItemPick())
            {
                Vanish();
            };
        }
    }
}