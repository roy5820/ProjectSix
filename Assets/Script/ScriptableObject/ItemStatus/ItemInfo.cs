using UnityEngine;
[CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo", order = 1)]
public class ItemInfo : ScriptableObject
{
    public string itemName = "ItemName";//아이템 이름
    public Sprite itemImg = null;//아이템 이미지
    public StateEnum state;//실행할 상태 값
    public int price = 100;//아이템 가격
    public float offense = 1.0f;//계수
    public int useCost = 0;//사용시 지불 코스트
    public int itemLevel = 0;//아이템 강화 단계
}
