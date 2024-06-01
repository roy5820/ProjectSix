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
        int playerIndex = _battleManager.GetPlatformIndexForObj(_battleManager.onPlayer.gameObject);
        int platformCnt = _battleManager.PlatformList.Length;
        int LDistance = playerIndex;//���� �÷������� ������ �Ÿ� ���
        int RDistance = Mathf.Abs((platformCnt - 1) - playerIndex);//������ �÷������� ������ �Ÿ� ���
        int teleportIndex = LDistance >= RDistance ? 0 : platformCnt - 1;//�ڷ���Ʈ ��ġ ���

        //Ž�� �� ���� �ش� Ÿ�Ϸ� ���� �̵�
        characterController.transform.position = _battleManager.GetStandingPos(teleportIndex);

        //�����̵� �� ���� ����
        characterController.Direction = LDistance >= RDistance ? CharacterDirection.Right : CharacterDirection.Left;

        //������ ���� ����
        characterController.TransitionState(StateEnum.EnemyReadyToState, AttackStateType, 2);

        yield return null;
    }
}
