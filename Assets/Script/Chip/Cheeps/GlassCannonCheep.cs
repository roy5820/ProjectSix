using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannonCheep : CheepBase
{
    public StateBase changeState = null;//변경할 상태
    //유리대포 칩 기능 구현
    //플레이어 + 적 받는 데미지 2배로 증가
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        if (changeState != null)
        {
            Debug.Log(_characterController);
            Debug.Log(StateEnum.Hit+", " +changeState);
            _characterController.ChangingStateByType(StateEnum.Hit, changeState);
        }
    }
}
