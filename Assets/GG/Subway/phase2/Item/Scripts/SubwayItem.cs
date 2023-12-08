using System.Collections;
using UnityEngine;

[System.Serializable]
public class SubwayItem : MonoBehaviour
{
    public ItemType itemType;
    public enum ItemType
    {
        // ������
        FLASHLIGHT,

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
        */
    }

    public Sprite itemImage; // ȹ�� �� �κ��丮 â�� ��� ������

    public int itemNum; // ������ ���� ��ȣ �ο�(1����)
    private bool isUsed; //��� �Ϸ� ����

    private Renderer pRenderer; //������ ��ƼŬ material

    [Space(10)]
    protected AudioSource itemAudioSrc;
    public AudioClip itemPickUpSound;

    protected virtual void Awake()
    {

        itemAudioSrc = this.gameObject.GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        if ((this.itemType != SubwayItem.ItemType.FLASHLIGHT) && (pRenderer = this.transform.Find("Particle System").GetComponent<Renderer>()))
        {
            Debug.Log("item renderer set");
        }
    }

    public bool Item_pick()
    {
        for (int i = 0; i < 3; i++)
        {
            if (SubwayInventory.instance.invScripts[i] == null)
            {
                Debug.Log("Pick " + this.gameObject.name);
                SubwayInventory.instance.invScripts[i] = this;
                SubwayInventory.instance.invIcons[i].sprite = itemImage;

                itemAudioSrc.clip = itemPickUpSound;
                itemAudioSrc.Play();
                return true;
            }
        }


        return false;
    }

    //trigger sphere �ݰ濡 ���� ������ �ڵ� pick up
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Player>().Is_MyPlayer())
            {
                if (Item_pick())
                {
                    Item_vanish();
                }
            }
        }

        else if (other.CompareTag("CompAI"))
        {
            Item_vanish();
        }

    }

    public bool Get_isUsed() //Inventory.rearrange()���� ����
    {
        return isUsed;
    }

    /*
    public void Set_isUsed(bool isUsed)
    {
        this.isUsed = isUsed;
    }
    */

    public virtual void Item_effect()
    {
        isUsed = true;

        if (this.itemType == SubwayItem.ItemType.INTERRUPT)
        {
            Item_grab();
        }
        //E�� ������ ��� �Լ� 
    }


    public void Item_grab() //��ô�ؾ� �ϴ� ������ ������ Ŭ������ ����ϴ� �Լ�
    {
        //�����ۿ� ���� Item Manager���� �޾ƿ� ������ ������ �ε��� ����
        int idx = 0;
        switch (this.itemNum)
        {
            case 4: //KnockDown
                idx = 0;
                break;
            case 5: //SlowDown
                idx = 1;
                break;
            case 7: //Death(���� ������)
                idx = 2;
                break;
            default:
                Debug.Log("grab return");
                return;
        }

        //player �տ� ������ ������Ʈ ��ȯ
        GameObject onHandPos = GameMgr.Instance.m_LocalPlayer.GetComponent<Player>().OnHand;
        GameObject grabbedItem = Instantiate(SubwayItemMgr.Instance.GrabbableItems[idx], onHandPos.transform.position, Quaternion.identity);
        grabbedItem.transform.SetParent(onHandPos.transform);
        //itemAudioSrc.clip = itemGrabSound;
        //itemAudioSrc.Play();

        //���� �� ��ô �غ�
        GameMgr.Instance.m_LocalPlayer.m_bIsThrow = true;

        SubwayInventory.instance.Active_AimPoint(true);

        Debug.Log("grab " + this.gameObject.name);
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

}