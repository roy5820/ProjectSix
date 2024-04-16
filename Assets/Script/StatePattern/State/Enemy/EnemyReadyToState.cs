using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//공격 준비 상태
public class EnemyReadyToState : StateBase
{
    bool isPreparing = false;//행동 준비 중 여부
    
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        characterController.AvailabilityOfAction = false;
        isPreparing = true;//행동 준비 여부 ture

        yield return new WaitForSeconds(sateDelayTime);//행동 선택 후딜레이

        //행동 준비 상태로 전환 후 행동 행동 종료
        characterController.TurnEnd();

        //턴 경과 체크
        while (true)
        {
            if (!isPreparing)
                break;
            yield return null;
        }
        characterController.AvailabilityOfAction = true;
        //매개 변수 값에 따라 행동 실행 여부 선택
        if (datas[0] != null)
            characterController.TransitionState((StateEnum)datas[0]);//행동 실행
    }

    //행동 준비 상태에서 적턴이 되면 isReady의 값을 true 하는 함수
    public void TurnStart()
    {
        if(isPreparing == true && !characterController.isDie)
            isPreparing = false;
            
    }
}
