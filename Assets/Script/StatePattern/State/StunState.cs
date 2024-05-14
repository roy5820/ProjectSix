using System;
using System.Collections;
using UnityEngine;
//ĳ���� �ǰ�
public class StunState : StateBase
{
    private bool isStun = false;//���� ����
    int stunTurn = 0;//���� �ð�

    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        isStun = true;
        characterController.isAvailabilityOfAction = false;//�ൿ�Ұ� ���·� ��ȯ
        //���� �ð� ����
        if (datas.Length > 0)
            stunTurn = Convert.ToInt32(datas[0]);
        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        //���� ���·� ��ȯ �� �� ���� ó��
        characterController.isStatusProcessing = false;
        characterController.TurnEnd();


        while (stunTurn > 0)
        {
            yield return null;
        }

        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isStun = false;
        characterController.isAvailabilityOfAction = true;//�ൿ���� ���·� ��ȯ
        yield return base.StateFuntion(datas);
    }

    //�� ���ῡ ���� ���� ����
    private void TurnEnd()
    {
        if (isStun)
        {
            stunTurn--;
        }
            
    }
}
