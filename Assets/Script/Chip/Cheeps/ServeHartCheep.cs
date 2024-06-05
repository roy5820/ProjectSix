using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeHartCheep : CheepBase
{
    public StateBase serveHartState = null;
    //���� ��Ʈ Ĩ ��� ����
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        //���� �� �� ���¸� ���� ��Ʈ ���·� �ٲ�
        if(serveHartState != null)
        {
            _characterController.ChangingStateByType(StateEnum.Die, serveHartState);
        }
    }
}
