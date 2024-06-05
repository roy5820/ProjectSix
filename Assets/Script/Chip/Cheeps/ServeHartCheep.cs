using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeHartCheep : CheepBase
{
    public StateBase serveHartState = null;
    //서브 하트 칩 기능 구현
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        //죽을 시 쓸 상태를 서브 하트 상태로 바꿈
        if(serveHartState != null)
        {
            _characterController.ChangingStateByType(StateEnum.Die, serveHartState);
        }
    }
}
