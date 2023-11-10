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
            if (Item_pick())
            {
                Item_vanish();
            };
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

        //E형 아이템 상속 함수 
        //I형 아이템 상속 함수
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