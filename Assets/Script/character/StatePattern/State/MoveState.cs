using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : StateBase
{
    private CharacterMovement moveController;//이동 구현 컨트롤러
    private CharacterDirection moveDirection { get; set; }//이동 방향
    private float movePower = 30f;//이동 속도

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        moveDirection = (CharacterDirection)datas[0];//입력 받은 캐릭터 이동방향 설정
        StartCoroutine(this.GetComponent<CharacterMovement>().StraightLineMovement((int)moveDirection, movePower, 1));//캐릭터 무브먼트를 사용하여 이동 구현
        yield return base.StateFuntion();
    }
}

