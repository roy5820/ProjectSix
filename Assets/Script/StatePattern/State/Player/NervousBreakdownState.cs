using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//신경계 폭주 아이템 구현
public class NervousBreakdownState : StateBase
{
    private Coroutine endCheckCoroutine = null;//
    private PlayerController playerController = null;
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
        yield return new WaitForSeconds(sateDelayTime);//딜레이 구현
        
        //폭주 여부에 따른 효과 처리
        //폭주 종료 처리
        if (playerController.isBreakdown)
        {
            characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            playerController.isBreakdown = false;
            StopCoroutine(endCheckCoroutine);
            characterController.TurnEnd();
        }
        //폭주 시작 처리
        else
        {
            characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            playerController.isBreakdown = true;
            endCheckCoroutine = StartCoroutine(BreakdownEndCheck());
        }

        yield return base.StateFuntion(datas);
    }

    //신경 폭주중 배터리 0이 될시 아이템 효과 종료 시키는 함수
    private IEnumerator BreakdownEndCheck()
    {
        while (playerController.NowBattery > 0)
        {
            yield return null;
        }
        StartCoroutine(StateFuntion());//신경 폭주 종료
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
