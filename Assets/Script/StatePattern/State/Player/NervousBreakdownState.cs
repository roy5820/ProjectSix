using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//신경계 폭주 아이템 구현
public class NervousBreakdownState : StateBase
{
    private Coroutine endCheckCoroutine = null;//
    private PlayerController playerController = null;
    public RuntimeAnimatorController normalAni = null;
    public RuntimeAnimatorController nervousBreakdwonAni = null;//신경계 폭주시 애니메이셪ㄴ
    //이벤트 등록
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, StopBreakdown);
    }

    //이벤트 해제
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, StopBreakdown);
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        playerController = (PlayerController)characterController;//플레이어 컨트롤러 가져오기

        //폭주 여부에 따른 효과 처리
        BackgroundEffectController.Instance.OnSwitchBackgroundAfterimage();//배경 잔상효과 적용
        //폭주 종료 처리
        if (playerController.isBreakdown)
        {
            while (true)
            {
                
                if (_animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
                {
                    if (normalAni)
                        _animator.runtimeAnimatorController = normalAni;
                    playerController.isBreakdown = false;
                    characterController.TurnEnd();
                    break; 
                }
                yield return null;
            }
            
        }
        //폭주 시작 처리
        else
        {
            if (nervousBreakdwonAni)
                _animator.runtimeAnimatorController = nervousBreakdwonAni;
            playerController.isBreakdown = true;
        }

        yield return base.StateFuntion(datas);
    }

    //신경 폭주 강제 종료 함수
    private void StopBreakdown()
    {
        if (playerController != null)
        {
            if (playerController.isBreakdown)
                StartCoroutine(StateFuntion());//신경 폭주 종료
        }
    }
}
