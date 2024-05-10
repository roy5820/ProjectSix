using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    PlayerStatus playerStatus;
    public int maxBattery = 10;
    public int nowBattery = 0;
    public int NowBattery
    {
        get
        {
            return nowBattery;
        }

        set
        {
            nowBattery = value;
            if(nowBattery > maxBattery)
                nowBattery = maxBattery;
        }
    }
    public int batteryRecoveryFigures = 1;
    //이벤트 등록
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, TurnStart);
    }

    //이벤트 해제
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, TurnStart);
    }
    protected override void Start()
    {
        base.Start();
        //상태값 초기화
        nowBattery = maxBattery / 2;
    }

    //턴 시작 처리
    public override void TurnStart()
    {
        base.TurnStart();
        //배틀메니저의 onPlayer값이 없으면 값 초기화
        if (_battleManager.onPlayer == null)
        {
            _battleManager.onPlayer = this;
        }
            
        isAvailabilityOfAction = true;
        NowBattery += batteryRecoveryFigures;
    }
}