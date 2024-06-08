using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChipBtnBase : MonoBehaviour, IPointerEnterHandler
{
    protected GameManager _gameManager;//게임 메니저
    public CheepInfo chipInfo = null;// 현재칩 정보
    private Image chipIcon;//칩아이콘을 적용할 이미지 객체
    public Sprite nullIcon;//빈칩 아이콘
    protected virtual void Start()
    {
        _gameManager = GameManager.Instance;//게임메니저 초기화
        chipIcon = transform.GetChild(0).GetComponent<Image>();//칩아이콘 표시할 이미지 객체값 초기화
    }

    //버튼 아이콘 이미지 갱신
    protected virtual void Update()
    {
        //CheepInfo의 icon이미지 적용
        if (chipInfo != null)
        {
            chipIcon.gameObject.SetActive(true);
            chipIcon.sprite = chipInfo.CheepIcon;
        }
        //nullIcon 이미지 적용
        else
        {
            chipIcon.gameObject.SetActive(false);
        }
    }

    //칩 버튼 클릭 시 이벤트 구현 함수
    public virtual void OnChipEvent()
    {
        
        //칩 인벤토리 데이터 조정 후 화면 칩 버튼 재구성
        ChipInventorySystem chipInventorySystem = GetComponentInParent<ChipInventorySystem>();
        
        chipInventorySystem.SetChipBtn();//칩 버튼을 게임 메니저의 cheepInventory값을 바탕으로 재구성하는 함수 호출
    }

    //마우스 포인터가 올라 올 경우 칩 정보창 수정
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(chipInfo != null)
        {
            ChipInventorySystem chipInventorySystem = GetComponentInParent<ChipInventorySystem>();//칩인벤토리 관리 시스템 가져오기
            chipInventorySystem.UpdateChipInfo(chipInfo);//칩 정보창 업데이트 함수 호출
        }    
    }
}
