using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

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

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TurnEventBus.Publish(TurnEventType.TurnEnd);
        }
    }
    //턴 시작 처리
    public override void TurnStart()
    {
        base.TurnStart();
        //배틀메니저의 onPlayer값이 없으면 값 초기화
        if (_battleManager.onPlayer == null)
        {
            _battleManager.onPlayer = this;
            Debug.Log("플레이어 값 초기화");
        }
            
        isAvailabilityOfAction = true;
    }

    //플레이어 턴 종료 처리
    public override void TurnEnd()
    {
        base.TurnEnd();
        TurnEventBus.Publish(TurnEventType.EnemyTurn);
    }
}