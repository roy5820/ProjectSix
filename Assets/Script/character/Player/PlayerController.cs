using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
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
            if(nowBattery > _characterStatus.maxBattery)
                nowBattery = _characterStatus.maxBattery;
        }
    }
    public int batteryRecoveryFigures = 1;//배터리 회복 량
    public bool isBreakdown = false;//폭주 여부
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
    protected void Start()
    {
        //상태값 초기화
        nowBattery = _characterStatus.maxBattery / 2;
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

    public override void StatusValueSetting()
    {
        //게임 메니저에 저장된 플레이어 스테이터스 값이 있으면
        if(gameManager.playerStatus != null)
        {
            _characterStatus = ScriptableObject.CreateInstance<CharacterStatus>();
            _characterStatus.maxHp = gameManager.playerStatus.maxHp;
            _characterStatus.nowHp = gameManager.playerStatus.nowHp;
            _characterStatus.maxBattery = _characterStatusOriginal.maxBattery;
            _characterStatus.offensePower = gameManager.playerStatus.offensePower;

            return;
        }

        base.StatusValueSetting();
    }

    public override void TurnEnd()
    {
        //신경폭주 상태일 경우 턴 종료 안됨
        if (isBreakdown)
        {
            NowBattery--;//행동 후 배터리 1 감소
            return;
        }
            
        base.TurnEnd();
    }

    private void OnDestroy()
    {
        TurnEventBus.Publish(TurnEventType.Lose);
    }
}