using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    private PlayerController _playerController;
    public Image itemImg;//아이템 이미지
    public Text itemText;//코스트 텍스트
    public StateEnum useState;//아이템 사용시 사용할 상태
    public float offense = 1f;//계수
    public int useCost = 5;//사용 시 지불 코스트

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//플레이어 객체 가져오기
    }

    private void Update()
    {
        itemText.text = useCost.ToString();//지불 코스트 갱신
    }

    //아이템 사용 처리
    public void ClickItem()
    {
        //아이템 사용 가능 여부 체크
        if (_playerController)
        {
            if (!_playerController.isStatusProcessing && _playerController.isTurnReady && _playerController.nowBattery >= useCost)
            {
                _playerController.TransitionState(useState, offense);//아이템 사용
                _playerController.nowBattery -= useCost;//코스트 지불
            }
        }
    }
}
