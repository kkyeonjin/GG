using System.Collections;
using UnityEngine;

[System.Serializable]
public class SubwayItem : MonoBehaviour
{
    public ItemType itemType;
    public enum ItemType
    {
        // �ڱ� ��ȭ
        ENFORCEMENT,
        /*
        (1) HP, //HP ȸ��
        (2) STAMINA, //���¹̳� ȸ�� (Max�� 30%p ����)
        (3) ORDER, //���������� ȸ��
        */

        // ��� ����
        INTERRUPT
        /*
        (4) KNOCKDOWN, //�Ѿ�߸��� (Max HP�� 15%p ����)
        (5) SLOWDOWN, //�޸��� �ӵ� �ݰ� 5��
        (*) SLOWDOWNALL //(�õ��) ��ü �÷��̾� �޸��� �ӵ� �ݰ� 5��
        */
    }

    public Sprite itemImage; // ȹ�� �� �κ��丮 â�� ��� ������

    public int itemNum; // ������ ���� ��ȣ �ο�(1����)
    public bool used; //��� �Ϸ� ����

    private Renderer pRenderer; //������ ��ƼŬ material

    protected virtual void Start()
    {
        pRenderer = this.transform.Find("Particle System").GetComponent<Renderer>();
    }

    public bool Item_pick()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SubwayInventory.instance.invScripts[i] == null)
            {
                Debug.Log("SubwayItem Pick");
                SubwayInventory.instance.invScripts[i] = this;
                SubwayInventory.instance.invIcons[i].sprite = itemImage;
                return true;
            }
        }
        return false;
    }

    public virtual void Item_effect()
    {
        used = true;
        if(itemType == ItemType.ENFORCEMENT) //��ȭ�� ������ -> ���
        {
            //���

        }
        else //Interrupt�� ������ -> ���� �� ��ô
        {
            //�տ� ���
            GameMgr.Instance.m_LocalPlayer.GetComponent<SubwayItem_OnHand>().Item_Grab(itemNum);
            
            //����
            GameMgr.Instance.m_LocalPlayer.m_bIsThrow = true;
        }

        Destroy(this.gameObject);
    }

    public void Item_vanish()
    {
        StartCoroutine("particleFadeOut");
        Destroy(this.gameObject.GetComponent<SphereCollider>());
        this.transform.Find("icon").gameObject.SetActive(false);
        this.transform.Find("Sphere").gameObject.SetActive(false);
    }

    IEnumerator particleFadeOut() //��ƼŬ �ܻ� ������ �������� ȿ��
    {
        int i = 20;
        while (i > 0) 
        {
            i--;
            float f = i / 10.0f;
            Color c = pRenderer.material.color;
            c.a = f;
            pRenderer.material.color = c;
            yield return new WaitForSeconds(0.02f);
        }

        //yield return new WaitForSeconds(0.8f);
        this.transform.Find("Particle System").gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    //trigger sphere �ݰ濡 ���� ������ �ڵ� pick up
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Item_pick())
            {
                Item_vanish();
            };
        }
    }
}