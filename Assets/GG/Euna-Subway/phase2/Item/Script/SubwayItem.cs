using System.Collections;
using UnityEngine;

[System.Serializable]
public class SubwayItem : MonoBehaviour
{
    public enum ItemType
    {
        // 자기 강화
        ENFORCEMENT,
        /*
        HP, //HP 회복
        STAMINA, //스태미나 회복 (Max의 30%p 증가)
        ORDER, //질서게이지 회복
        */

        // 상대 방해
        INTERRUPT
        /*,
        KNOCKDOWN, //넘어뜨리기 (Max HP의 15%p 감소)
        SLOWDOWN, //달리기 속도 반감 5초
        SLOWDOWNALL //(꼴등용) 전체 플레이어 달리기 속도 반감 5초
        */
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public virtual void Use_Item()
    {
        // return false; //아이템 사용 성공 여부
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

    //trigger sphere 반경에 들어가면 아이템 자동 pick up
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