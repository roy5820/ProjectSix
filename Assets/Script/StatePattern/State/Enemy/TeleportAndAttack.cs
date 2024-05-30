using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//플레이어와 가장 거리가 먼 타일로 순간 이동 후 공격 실행
public class TeleportAndAttack : StateBase
{
    public StateEnum AttackStateType;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //플레이어와 가장 거리가 먼 타일 탐색

        //탐색 후 순간 해당 타일로 순간 이동


        //설정된 공격 실행
        characterController.TransitionState(StateEnum.EnemyReadyToState, AttackStateType);

        yield return null;
    }
}
