using System.Collections;
using UnityEngine;
//캐릭터 방향 전환
public class TurnaboutState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //방향 전환 기능 구현
        characterController.Direction = characterController.Direction == CharacterDirection.Right ? CharacterDirection.Left : CharacterDirection.Right;

        yield return new WaitForSeconds(sateDelayTime);//방향전환 후 딜레이

        characterController.TurnEnd();//상태 종료 시 턴 종료

        yield return base.StateFuntion(datas);
    }
}

