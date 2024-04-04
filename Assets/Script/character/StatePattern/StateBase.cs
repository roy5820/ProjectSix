using System.Collections;
using UnityEngine;

public class StateBase : MonoBehaviour, CharacterState
{
    protected CharacterController characterController;//캐릭터 컨트롤러

    public void Handle(CharacterController characterController)
    {
        if (!this.characterController)
            this.characterController = characterController;

        StartCoroutine(SateFuntion());//기능 구현 코루틴 함수 호출
    }

    //상속 받아 기능을 구현할 부분
    protected virtual IEnumerator SateFuntion()
    {
        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return null;
    }
}
