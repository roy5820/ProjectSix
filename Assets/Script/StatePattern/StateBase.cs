using System.Collections;
using UnityEngine;

public abstract class StateBase : MonoBehaviour, CharacterState
{
    protected CharacterController characterController = null;//캐릭터 컨트롤러
    protected BattleManager _battleManager = null;//배틀 매니저
    protected Animator _animator = null;//캐릭터 애니메이션

    public string stateAniParamater = "";
    public float sateDelayTime = 0.5f;//상태 딜레이 시간

    public void Handle(CharacterController characterController, params object[] datas)
    {
        //캐릭터 컨트롤러 값 초기화
        if (!this.characterController)
            this.characterController = characterController;
        //배틀 매니저 값 초기화
        if(!this._battleManager)
            this._battleManager = BattleManager.Instance;
        //캐릭터 애니메이터 가져오기
        if (!this._animator)
            TryGetComponent<Animator>(out _animator);
        //캐릭터 행동 애니메이션 출력
        if (!string.IsNullOrEmpty(stateAniParamater) && this._animator)
            _animator.SetTrigger(stateAniParamater);
         
        characterController.isStatusProcessing = true;//캐릭터 상태 처리 중

        StartCoroutine(StateFuntion(datas));//기능 구현 코루틴 함수 호출
    }

    //상속 받아 기능을 구현할 부분
    protected virtual IEnumerator StateFuntion(params object[] datas)
    {
        characterController.isStatusProcessing = false;//캐릭터 상태 처리 종료
        yield return null;
    }
}
