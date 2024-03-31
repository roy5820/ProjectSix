using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<StageInfo> stageList;//스테이지 정보를 담은 리스트

    public List<WaveInfo> nowStage;//현제 스테이지 정보를 담을 리스트
    private string turnState;

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        GameFlowEventBus.Subscribe(GameFlowType.BattleStart, BattleStart);//BattleStart 이벤트 설정
        GameFlowEventBus.Subscribe(GameFlowType.BattleEnd, BattleEnd);//BattleEnd 이벤트 설정

        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        GameFlowEventBus.Unsubscribe(GameFlowType.BattleStart, BattleStart);//BattleStart 이벤트 제거
        GameFlowEventBus.Unsubscribe(GameFlowType.BattleEnd, BattleEnd);//BattleEnd 이벤트 제거

        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 제거
    }

    //배틀 시작 시 이벤트 처리
    public void BattleStart()
    {


        TurnEventBus.Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }

    //배틀 종료 시 이벤트 처리
    public void BattleEnd()
    {

    }

    //턴 시작 시 이벤트 처리
    public void TurnStart()
    {
        turnState = "TurnStart";
    }

    //플레이어 턴 시작 시 이벤트 처리
    public void PlayerTurn()
    {
        turnState = "PlayerTurn";
    }


    //적턴 시작 시 이벤트 처리
    public void EnemyTurn()
    {
        turnState = "EnemyTurn";
    }

    //턴 종료 시 이벤트 처리
    public void TurnEnd()
    {
        turnState = "TurnEnd";
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "TURN STATUS: " + turnState);
    }
}
