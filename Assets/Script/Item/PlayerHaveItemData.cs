using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHaveItemData
{
    public int itemId = 0;//아이템 ID 100: 공격, 200: 방어, 300: 특수
    public int itemLevel = 0;//아이템 레벨
    public ItemInfo itemInfo;//아이템 정보
}
