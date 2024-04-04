using System.Collections;
using UnityEngine;

public class MoveState : StateBase
{
    private CharacterMovement moveController;//이동 구현 컨트롤러
    public CharacterDirection moveDirection { get; set; }//이동 방향
    private float movePower = 30f;//이동 속도

    protected override IEnumerator SateFuntion()
    {
        StartCoroutine(this.GetComponent<CharacterMovement>().StraightLineMovement((int)moveDirection, movePower, 1));//캐릭터 무브먼트를 사용하여 이동 구현
        yield return null;
    }
}

