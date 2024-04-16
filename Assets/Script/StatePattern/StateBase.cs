using System.Collections;
using UnityEngine;

public abstract class StateBase : MonoBehaviour, CharacterState
{
    protected CharacterController characterController = null;//캐릭터 컨트롤러
    protected GameManager _gameManager = null;//게임 메니저
    protected Animator _animator = null;//캐릭터 애니메이션

    public void Handle(CharacterController characterController, params object[] datas)
    {
        //캐릭터 컨트롤러 값 초기화
        if (!this.characterController)
            this.characterController = characterController;
        //캐릭터 메니저 값 초기화
        if (!this._gameManager)
            this._gameManager = GameManager.Instance;
        //캐릭터 애니메이터 가져오기
        if (!this._animator)
            TryGetComponent<Animator>(out _animator);

        StartCoroutine(StateFuntion(datas));//기능 구현 코루틴 함수 호출
    }

    //상속 받아 기능을 구현할 부분
    protected abstract IEnumerator StateFuntion(params object[] datas);
}
