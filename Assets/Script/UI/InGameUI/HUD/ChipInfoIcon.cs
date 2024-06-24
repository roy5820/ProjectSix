using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipInfoIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CheepInfo chipInfo;
    public GameObject chipInspectorPre;//아이템 정보 창
    private GameObject popupPre = null;//생성한 팝업창

    private void Start()
    {
        //칩정보를 토데로 값 설정
        if(chipInfo != null) {
            GetComponent<Image>().sprite = chipInfo.CheepIcon;
        }
    }

    //마우스 포인터가 올라 올 경우 아이템 인스펙터 창을 띄우는 이벤트
    public void OnPointerEnter(PointerEventData eventData)
    {
        chipInspectorPre.GetComponent<ChipInfoWIndow>().chipInfo = chipInfo;
        popupPre = Instantiate(chipInspectorPre, this.transform.parent.parent);
    }

    //마우스 포인터가 벗어 날 경우 앙이템 인스펙터 창을 없애는 이벤트
    public void OnPointerExit(PointerEventData eventData)
    {
        if (popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }
    }
}
