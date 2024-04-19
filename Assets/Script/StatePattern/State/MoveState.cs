using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
//캐릭터 이동
public class MoveState : StateBase
{
    private CharacterMovement moveController;//이동 구현 컨트롤러
    private CharacterDirection moveDirection { get; set; }//이동 방향
    public float movePower = 30f;//이동 속도

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        CharacterMovement movement = GetComponent<CharacterMovement>();//캐릭터 무브먼트 가져오기
        moveDirection = (CharacterDirection)datas[0];//입력 받은 캐릭터 이동방향 설정
        movement.moveCoroutine = StartCoroutine(movement.StraightLineMovement((int)moveDirection, movePower, 1));//캐릭터 무브먼트를 사용하여 이동 구현

        //이동중 턴 종료 방지
        while (movement.moveCoroutine != null)
        {
            yield return null;
        }

        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}

