using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�÷��̾�� ���� �Ÿ��� �� Ÿ�Ϸ� ���� �̵� �� ���� ����
public class TeleportAndAttack : StateBase
{
    public StateEnum AttackStateType;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //�÷��̾�� ���� �Ÿ��� �� Ÿ�� Ž��

        //Ž�� �� ���� �ش� Ÿ�Ϸ� ���� �̵�


        //������ ���� ����
        characterController.TransitionState(StateEnum.EnemyReadyToState, AttackStateType);

        yield return null;
    }
}
