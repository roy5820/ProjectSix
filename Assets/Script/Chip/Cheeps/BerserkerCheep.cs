using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerserkerCheep : CheepBase
{
    public StateBase damageWhenMovingState = null;//�̵� �� ������ �ο� ����
    public StateBase recoveHPWhenKillState = null;//óġ �� HP ȸ��
    //����Ŀ ��� ����
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        //damageWhenMovingState�� �̵��� ���·� ����
        if (damageWhenMovingState != null)
        {
            Debug.Log("ü����");
            _characterController.ChangingStateByType(StateEnum.Move, damageWhenMovingState);
        }
        //recoveHPWhenKillState�� �� ���� �� ���·� ����
        if (recoveHPWhenKillState != null)
        {
            _characterController.ChangingStateByType(StateEnum.Die, recoveHPWhenKillState);
        }
    }
}
