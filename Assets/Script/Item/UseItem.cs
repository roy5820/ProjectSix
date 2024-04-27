using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    private PlayerController _playerController;
    public StateEnum useState;//아이템 사용시 사용할 상태
    public int cost = 5;

    private void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    //아이템 사용 처리
    public void ClickItem()
    {

        if(!_playerController.isStatusProcessing && _playerController.isTurnReady && _playerController.nowBattery >= cost)
        {
            _playerController.TransitionState(useState);
            _playerController.nowBattery -= cost;
        }
            
    }
}
