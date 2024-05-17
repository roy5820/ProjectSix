using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int itemId;//아이템 ID 100: 공격, 200: 방어, 300: 특수
    public string itemName;//아이템 이름
    public List<ItemInfo> itemInfo;//아이템 정보 리스트
}
