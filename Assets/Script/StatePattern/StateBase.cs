using System.Collections;
using UnityEngine;

public class StateBase : MonoBehaviour, CharacterState
{
    protected CharacterController characterController;//캐릭터 컨트롤러
    protected GameManager _gameManager;//게임 메니저

    public void Handle(CharacterController characterController, params object[] datas)
    {
        //캐릭터 컨트롤러 값 초기화
        if (!this.characterController)
            this.characterController = characterController;
        //캐릭터 메니저 값 초기화
        if (!this._gameManager)
            this._gameManager = GameManager.Instance;
        StartCoroutine(StateFuntion(datas));//기능 구현 코루틴 함수 호출
    }

    //상속 받아 기능을 구현할 부분
    protected virtual IEnumerator StateFuntion(params object[] datas)
    {
        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return null;
    }
}
