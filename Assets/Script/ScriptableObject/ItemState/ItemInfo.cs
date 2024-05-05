using UnityEngine;
[CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo", order = 1)]
public class ItemInfo : ScriptableObject
{
    public string itemName = "ItemName";//아이템 이름
    public Sprite itemImg = null;//아이템 이미지
    public GameObject itemObj = null;//아이템 오브젝트
    public int price = 100;//아이템 가격
    public int useCost = 0;//사용시 지불 코스트
    public int itemLevel = 0;//아이템 강화 단계
}
