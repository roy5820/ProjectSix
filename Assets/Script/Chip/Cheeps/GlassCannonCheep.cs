using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCannonCheep : CheepBase
{
    public StateBase changeState = null;//������ ����
    //�������� Ĩ ��� ����
    //�÷��̾� + �� �޴� ������ 2��� ����
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
