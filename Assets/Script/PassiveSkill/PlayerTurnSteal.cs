using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

public class PlayerTurnSteal : MonoBehaviour
{
    private BattleManager _battleManager;//배틀매니저
    public float steelCastingTime = 10;//스킬 시간
    private bool isPlayerTurn = false;//플레이어 턴 여부
    private Coroutine stillTurnCoroutine = null;//턴 스틸 구현할 코루틴이 담길 변수

    private void Start()
    {
        _battleManager = BattleManager.Instance;//배틀매니저 값 초기화
    }

    //이벤트 등록
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurnStart);
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, PlayerTurnEnd);
    }

    //이벤트 해제
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurnStart);
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, PlayerTurnEnd);
    }

    //플레이어 턴 시작 시 이벤트 처리 함수
    private void PlayerTurnStart()
    {
        stillTurnCoroutine = StartCoroutine(StillTUrn());//플레이어 턴 스틸 구현 코루틴 호출
    }

    //플레이어 턴 종료 시 이벤트 처리 함수
    private void PlayerTurnEnd()
    {
        //코루틴 진행 중일 경우 강제 종료
        if (stillTurnCoroutine != null)
            StopCoroutine(stillTurnCoroutine);
    }

    //턴 스틸 구현 코루틴 함수
    private IEnumerator StillTUrn()
    {
        yield return new WaitForSeconds(steelCastingTime);//턴 스틸 캐스팅 구현
        TurnEventBus.Publish(TurnEventType.EnemyTurn);//플레이어 턴 종료
    }
}
