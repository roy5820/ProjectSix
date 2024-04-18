using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : CharacterController
{
    //적이 사용할 상태에 대한 정보
    [System.Serializable]
    public class StateCondition
    {
        public StateEnum stateEnum; // 상태 열거형
        public float range; // 사거리, 99는 사거리 없음
        public float cooldown; // 쿨타임, 0 쿨타임 없음
        public float nowCoolTIme;//현제 쿨타임
        public bool NeedToPrepare;//파라미터
    }

    public List<StateCondition> stateConditions; // 상태와 조건의 리스트

    //이벤트 등록
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, TurnStart);
    }

    //이벤트 해제
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, TurnStart);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TurnStart()
    {
        
        base.TurnStart();
        GetComponent<EnemyReadyToState>().TurnStart();//공격 딜레이 구현 상태에게 turnStart상태 알림
        //전턴 준비중인 행동이 없으면 실행
        if (AvailabilityOfAction)
        {
            //적 행동 쿨타임 돌리기
            foreach (StateCondition condition in stateConditions)
            {
                //스킬 사용 가능 여부 체크
                if (condition.nowCoolTIme > 0)
                {
                    condition.nowCoolTIme--;//쿨타임 감소
                }
            }

            StateEnum selectStateEnum = SelectState();//enemy턴이 되었을 때 행동가능 상태면 해동 실행
        }
        else if(!isDie)
            TurnEnd();
        
    }

    //stateConditions리스트에서 사용가능 한 상태를 우선순위에 따라 찾아 해당 상태 열거형을 반환
    private StateEnum SelectState()
    {
        StateEnum stateEnum = 0;//사용할 상태 실행함수 이름

        //적과의 사거리 계산하기
        int distance = 99;//플레이어와의 거리
        int thisIndex = gameManager.GetPlatformIndexForObj(gameObject);//현재 위치 값 가져오기
        int countDistance = 0;//플레이어 와의 거리를 카운트할 값이 저장되는 변수
        //플레이어 와의 거리 가져오기
        for (int i = thisIndex + (direction == CharacterDirection.Right ? 1: -1); direction == CharacterDirection.Right ? i < gameManager.PlatformList.Length : i >= 0; i += (direction == CharacterDirection.Right ? 1 : -1))
        {
            
            countDistance++;
            GameObject targetObj = gameManager.GetOnPlatformObj(i);
            if(targetObj != null)
            {
                //경로상 플레이어가 있으면 거리 갱신
                if (targetObj.layer == LayerMask.NameToLayer("Player"))
                {
                    distance = countDistance;
                    break;
                }
            }
        }

        //우선 순위에 따른 적 행동 선택
        foreach (StateCondition condition in stateConditions)
        {
            //스킬 사용 가능 여부 체크
            if (condition.nowCoolTIme == 0 && condition.range >= distance)
            {
                condition.nowCoolTIme = condition.cooldown;//쿨타임 적용

                //준비해야하는 스킬인지에 따른 상태 처리
                if (condition.NeedToPrepare)
                    TransitionState(StateEnum.EnemyReadyToState, condition.stateEnum);//상태 실행
                else
                    TransitionState(condition.stateEnum);//상태 실행
                break;
            }
        }

        return stateEnum;
    }

    //Enemy 턴 종료 처리
    public override void TurnEnd()
    {
        BattleManager.Instance.OnTurnOverEnemyCnt++;//턴종료된 적 카운트 ++
        base.TurnEnd();//턴 앤드 처리
    }
}
