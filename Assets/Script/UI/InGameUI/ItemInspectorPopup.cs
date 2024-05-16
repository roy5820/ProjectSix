using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspectorPopup : MonoBehaviour
{
    public ItemInfo itemInfo;//아이템 정보
    public Text itemName;//아이템 이름
    public Text itemOffenseTxt;//아이템 수치 명
    public Text itemOffense;//아이템 수치
    public Text itemCoast;//아이템 코스트
    public Text itemEffect;//아이템 효과
    // Start is called before the first frame update
    void Start()
    {
        //아이템 정보 갱신
        itemName.text = itemInfo.itemName;//아이템 명
        itemOffense.text = (itemInfo.offense).ToString();//아이템 데미지
        //아이템 타입에 따른 아이템 수치 표시
        int itemType = itemInfo.itemType;// 아이템 타입 가져오기
        string offenseTxt = null;//표시할 수치 명
        int offense = (int)itemInfo.offense;//표시할 수치
        switch (itemType)
        {
            case 0:
                offenseTxt = "데미지: ";
                offense = (int)(BattleManager.Instance.onPlayer._characterStatus.offensePower * itemInfo.offense);//데미지 계산
                break;
            case 1:
                offenseTxt = "지속시간: ";
                break;
            case 2:
                offenseTxt = "";
                break;
        }
        itemOffenseTxt.text = offenseTxt;//수치명
        itemOffense.text = offense.ToString();//수치
        itemCoast.text = (itemInfo.useCost).ToString();//사용 코스트
        itemEffect.text = itemInfo.effectDescription;//아이템 효과
    }
}
