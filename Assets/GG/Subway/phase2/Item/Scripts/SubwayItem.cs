using System.Collections;
using UnityEngine;

[System.Serializable]
public class SubwayItem : MonoBehaviour
{
    public ItemType itemType;
    public enum ItemType
    {
        // 손전등
        FLASHLIGHT,

        // 자기 강화
        ENFORCEMENT,
        /*
        (1) HP, //HP 회복
        (2) STAMINA, //스태미나 회복 (Max의 30%p 증가)
        (3) ORDER, //질서게이지 회복
        */

        // 상대 방해
        INTERRUPT
        /*
        (4) KNOCKDOWN, //넘어뜨리기 (Max HP의 15%p 감소)
        (5) SLOWDOWN, //달리기 속도 반감 5초
        */
    }

    public Sprite itemImage; // 획득 시 인벤토리 창에 띄울 아이콘

    public int itemNum; // 아이템 고유 번호 부여(1부터)
    private bool isUsed; //사용 완료 여부

    private Renderer pRenderer; //아이템 파티클 material

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

    //trigger sphere 반경에 들어가면 아이템 자동 pick up
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

    public bool Get_isUsed() //Inventory.rearrange()에서 참조
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
        //E형 아이템 상속 함수 
    }


    public void Item_grab() //투척해야 하는 방해형 아이템 클래스만 상속하는 함수
    {
        //아이템에 따라 Item Manager에서 받아올 아이템 프리팹 인덱스 조정
        int idx = 0;
        switch (this.itemNum)
        {
            case 4: //KnockDown
                idx = 0;
                break;
            case 5: //SlowDown
                idx = 1;
                break;
            case 7: //Death(상점 아이템)
                idx = 2;
                break;
            default:
                Debug.Log("grab return");
                return;
        }

        //player 손에 아이템 오브젝트 소환
        GameObject onHandPos = GameMgr.Instance.m_LocalPlayer.GetComponent<Player>().OnHand;
        GameObject grabbedItem = Instantiate(SubwayItemMgr.Instance.GrabbableItems[idx], onHandPos.transform.position, Quaternion.identity);
        grabbedItem.transform.SetParent(onHandPos.transform);
        //itemAudioSrc.clip = itemGrabSound;
        //itemAudioSrc.Play();

        //조준 및 투척 준비
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

    IEnumerator particleFadeOut() //파티클 잔상 서서히 없어지는 효과
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