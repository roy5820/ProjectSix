using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
//ĳ���� �ǰ�
public class StunState : StateBase
{
    private bool isStun = false;//���� ����
    int stunTurn = 0;//���� �ð�
    public GameObject sturnEffectPre;//���� ǥ�� ������
    public Transform effectPos = null;//����Ʈ ���� ��ġ
    public Color stunColor = Color.blue;//���Ͻ� ����
    public GameObject effectList = null;
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
        //���� ����Ʈ ����
        GameObject sturnEffect = Instantiate(sturnEffectPre, effectPos.position, Quaternion.identity, effectList.transform);

        //���� ���·� ��ȯ �� �� ���� ó��
        characterController.isStatusProcessing = false;
        characterController.TurnEnd();

        while (stunTurn > 0)
        {
            yield return null;
        }

        //����Ʈ ����
        Destroy(sturnEffect.gameObject);

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

