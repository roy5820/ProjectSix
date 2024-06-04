using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UseItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerController _playerController;
    public ItemInfo itemInfo;
    public Image itemImg;//아이템 이미지
    public Text itemText;//코스트 텍스트
    public StateEnum useState;//아이템 사용시 사용할 상태
    public float offense = 1f;//계수
    public int useCost = 5;//사용 시 지불 코스트
    public GameObject itemInspectorPre;//아이템 정보 창
    private GameObject popupPre = null;//생성한 팝업창

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//플레이어 객체 가져오기
        //itemInfo의 정보를 토대로 값 초기화
        if(itemInfo != null)
        {
            itemImg.sprite = itemInfo.itemImg;//아이템 이미지=
            offense = itemInfo.offense;//아이템 계수
            //지불 코스트
            useCost = itemInfo.useCost;
            itemText.text = itemInfo.useCost.ToString();
            useState = itemInfo.state;//상할 상태
        }
    }

    //아이템 사용 처리
    public void ClickItem()
    {
        //아이템 사용 가능 여부 체크
        if (_playerController)
        {
            if (!_playerController.isStatusProcessing && _playerController.isTurnReady && (_playerController.nowBattery >= useCost || _playerController.isBreakdown))
            {
                //폭주 상태가 아닐경우 코스트 지불
                if (!_playerController.isBreakdown)
                    _playerController.nowBattery -= useCost;//코스트 지불
                _playerController.TransitionState(useState, offense);//아이템 사용
            }
        }
    }

    //마우스 포인터가 올라 올 경우 아이템 인스펙터 창을 띄우는 이벤트
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInspectorPre.GetComponent<ItemInspectorPopup>().itemInfo = itemInfo;
        popupPre = Instantiate(itemInspectorPre, this.transform.parent.parent.parent);
    }

    //마우스 포인터가 벗어 날 경우 앙이템 인스펙터 창을 없애는 이벤트
    public void OnPointerExit(PointerEventData eventData)
    {
        if(popupPre != null)
        {
            Destroy(popupPre);
            popupPre = null;
        }
    }
}