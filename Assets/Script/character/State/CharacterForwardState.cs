using UnityEngine;

public class CharacterForwardState : StateBase
{
    private CharacterMovement moveController;//이동 구현 컨트롤러
    private CharacterDirection moveDirection;//이동 방향
    private float moveFower = 30f;//이동 속도
    public override void Handle(CharacterController characterController)
    {
        base.Handle(characterController);

        //해당 상태의 기능 구현 부분
        moveController = GetComponent<CharacterMovement>();//캐릭터 이동 구현 컴포넌트 가져오기
        moveDirection = characterController.direction;

        StartCoroutine(moveController.StraightLineMovement((int)moveDirection, moveFower, 1));//이동 구현 코루틴 호출
        
    }
}

