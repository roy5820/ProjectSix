using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : CharacterController
{
    public int dropMoney = 50;//몬스터 처치 시 드랍하는 돈


    //적이 사용할 상태에 대한 정보
    [System.Serializable]
    public class StateCondition
    {
        public StateEnum stateEnum; // 상태 열거형
        public float range; // 사거리, 99는 사거리 없음
        public int cooldown; // 쿨타임, 0 쿨타임 없음
        public int nowCoolTime;//현제 쿨타임
        public int delayTurn;//상태 딜레이 턴
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

    protected void Start()
    {
        DirectionSetting();
    }

    public override void TurnStart()
    {
        base.TurnStart();
        //전턴 준비중인 행동이 없으면 실행
        if (isAvailabilityOfAction && delayTurn == 0)
        {
            //적 행동 쿨타임 돌리기
            foreach (StateCondition condition in stateConditions)
            {
                //스킬 사용 가능 여부 체크
                if (condition.nowCoolTime > 0)
                {
                    condition.nowCoolTime--;//쿨타임 감소
                }
            }

            StateEnum selectStateEnum = SelectState();//enemy턴이 되었을 때 행동가능 상태면 해동 실행
        }
        //차징 종료 후 EnemyReadyToState에서 인식하여 준비하던 상태 실행
        else if (delayTurn > 0)
        {
            delayTurn--;
            if(delayTurn > 0)
                TurnEnd();
        }
        else
            TurnEnd();
    }

    public override void TurnEnd()
    {
        base.TurnEnd();
        _battleManager.ReadyForEnemy();
    }

    //stateConditions리스트에서 사용가능 한 상태를 우선순위에 따라 찾아 해당 상태 열거형을 반환
    private StateEnum SelectState()
    {
        StateEnum stateEnum = 0;//사용할 상태 실행함수 이름

        //적과의 사거리 계산하기
        int distance = 99;//플레이어와의 거리
        int thisIndex = _battleManager.GetPlatformIndexForObj(gameObject);//현재 위치 값 가져오기
        int countDistance = 0;//플레이어 와의 거리를 카운트할 값이 저장되는 변수
        //플레이어 와의 거리 가져오기
        for (int i = thisIndex + (Direction == CharacterDirection.Right ? 1: -1);
            Direction == CharacterDirection.Right ? i < _battleManager.PlatformList.Length : i >= 0;
            i += (Direction == CharacterDirection.Right ? 1 : -1))
        {
            
            countDistance++;
            GameObject targetObj = _battleManager.GetOnPlatformObj(i);
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
            if (condition.nowCoolTime == 0 && condition.range >= distance)
            {
                condition.nowCoolTime = condition.cooldown;//쿨타임 적용
                Debug.Log(condition.stateEnum);
                //준비해야하는 스킬인지에 따른 상태 처리
                if (condition.delayTurn > 0)
                    TransitionState(StateEnum.EnemyReadyToState, condition.stateEnum, condition.delayTurn);//상태 실행
                else
                    TransitionState(condition.stateEnum);//상태 실행
                break;
            }
        }

        return stateEnum;
    }

    private void OnDestroy()
    {
        //적 유닛 죽을 시 dropMoney만큼 스테이지 보상 증가
        _battleManager.stageRewards += dropMoney;
    }

    private void DirectionSetting()
    {
        //적 스폰 시 플레이어를 바라보는 방향으로 전환 시키기
        float targetPos = _battleManager.onPlayer.gameObject.transform.position.x;//플레이어 위치 가져오기
        float thisPos = gameObject.transform.position.x;//해당 객체의 위치 가져오기
        Direction = targetPos < thisPos ? CharacterDirection.Left : CharacterDirection.Right;
    }


}
