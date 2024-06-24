using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �غ� ����
public class EnemyReadyToState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        characterController.delayTurn = (int)datas[1];//���� �غ� �� ����
        //���� �غ����� ������ �ൿ �������� ���� �κ�
        EnemyHUDController eHUD = characterController.GetComponent<EnemyHUDController>();
        
        eHUD.OnActionIcon((StateEnum)datas[0]);

        //�ൿ �غ� ���·� ��ȯ �� �ൿ �ൿ ����
        characterController.isStatusProcessing = false;
        characterController.TurnEnd();
        //�� ��� üũ
        while (true)
        {
            //��¡ ���� �� ���� ����
            if (characterController.delayTurn == 0 && characterController.isAvailabilityOfAction)
                break;
                
            yield return null;
        }
        //�ൿ ������ ���� �κ�
        eHUD.OffActionIcon();

        characterController.delayTurn = 0;//���� �غ� ���� false

        //�Ű� ���� ���� ���� �ൿ ���� ���� ����
        if (datas[0] != null)
            characterController.TransitionState((StateEnum)datas[0]);//�ൿ ����
    }
}
