using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerCheep : CheepBase
{
    public StateBase damageWhenMovingState = null;//이동 시 데미지 부여 상태
    public StateBase recoveHPWhenKillState = null;//처치 시 HP 회복
    //버서커 기능 구현
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        //damageWhenMovingState를 이동시 상태로 변경
        if (damageWhenMovingState != null)
        {
            Debug.Log("체인지");
            _characterController.ChangingStateByType(StateEnum.Move, damageWhenMovingState);
        }
        //recoveHPWhenKillState를 적 죽을 시 상태로 변경
        if (recoveHPWhenKillState != null)
        {
            _characterController.ChangingStateByType(StateEnum.Die, recoveHPWhenKillState);
        }
    }
}
