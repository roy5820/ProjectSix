using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//플레이어와 가장 거리가 먼 타일로 순간 이동 후 공격 실행
public class TeleportAndAttack : StateBase
{
    public StateEnum AttackStateType;//공격시 실행할 상태 타입
    public int AttackDelayTurn = 2;//공격 선딜레이 턴
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //플레이어와 가장 거리가 먼 타일 탐색
        int playerIndex = _battleManager.GetPlatformIndexForObj(_battleManager.onPlayer.gameObject);
        int platformCnt = _battleManager.PlatformList.Length;
        int LDistance = playerIndex;//왼쪽 플렛폼으로 부터의 거리 계산
        int RDistance = Mathf.Abs((platformCnt - 1) - playerIndex);//오른쪽 플렛폼으로 부터의 거리 계산
        int teleportIndex = LDistance >= RDistance ? 0 : platformCnt - 1;//텔로포트 위치 계산

        //탐색 후 순간 해당 타일로 순간 이동
        characterController.transform.position = _battleManager.GetStandingPos(teleportIndex);

        //순간이동 후 방향 조정
        characterController.Direction = LDistance >= RDistance ? CharacterDirection.Right : CharacterDirection.Left;

        //설정된 공격 실행
        characterController.TransitionState(StateEnum.EnemyReadyToState, AttackStateType, 2);

        yield return null;
    }
}
