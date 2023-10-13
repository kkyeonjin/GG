using UnityEngine;



[System.Serializable]
public class SubwayItem
{
    public enum ItemType
    {
        // 자기 강화
        HP, //HP 회복
        STAMINA, //스태미나 회복 (Max의 30%p 증가)
        ORDER, //질서게이지 회복

        // 상대 방해
        KNOCKDOWN, //넘어뜨리기 (Max HP의 15%p 감소)
        SLOWDOWN, //달리기 속도 반감 5초
        SLOWDOWNALL //(꼴등용) 전체 플레이어 달리기 속도 반감 5초
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false; //아이템 사용 성공 여부
    }
}