using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�÷��̾�� ���� �Ÿ��� �� Ÿ�Ϸ� ���� �̵� �� ���� ����
public class TeleportAndAttack : StateBase
{
    public StateEnum AttackStateType;//���ݽ� ������ ���� Ÿ��
    public int AttackDelayTurn = 2;//���� �������� ��
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //�÷��̾�� ���� �Ÿ��� �� Ÿ�� Ž��


        //Ž�� �� ���� �ش� Ÿ�Ϸ� ���� �̵�
        

        //������ ���� ����
        characterController.TransitionState(StateEnum.EnemyReadyToState, AttackStateType, 2);

        yield return null;
    }
}
