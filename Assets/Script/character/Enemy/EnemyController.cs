using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : CharacterController
{
    public int dropMoney = 50;//���� óġ �� ����ϴ� ��


    //���� ����� ���¿� ���� ����
    [System.Serializable]
    public class StateCondition
    {
        public StateEnum stateEnum; // ���� ������
        public float range; // ��Ÿ�, 99�� ��Ÿ� ����
        public int cooldown; // ��Ÿ��, 0 ��Ÿ�� ����
        public int nowCoolTime;//���� ��Ÿ��
        public int delayTurn;//���� ������ ��
    }

    public List<StateCondition> stateConditions; // ���¿� ������ ����Ʈ

    //�̺�Ʈ ���
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, TurnStart);
    }

    //�̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, TurnStart);
    }

    protected void Start()
    {
        DirectionSetting();
    }

    public override void TurnStart()
    {
        base.TurnStart();
        //���� �غ����� �ൿ�� ������ ����
        if (isAvailabilityOfAction && delayTurn == 0)
        {
            //�� �ൿ ��Ÿ�� ������
            foreach (StateCondition condition in stateConditions)
            {
                //��ų ��� ���� ���� üũ
                if (condition.nowCoolTime > 0)
                {
                    condition.nowCoolTime--;//��Ÿ�� ����
                }
            }

            StateEnum selectStateEnum = SelectState();//enemy���� �Ǿ��� �� �ൿ���� ���¸� �ص� ����
        }
        //��¡ ���� �� EnemyReadyToState���� �ν��Ͽ� �غ��ϴ� ���� ����
        else if (delayTurn > 0)
        {
            delayTurn--;
            if(delayTurn > 0)
                TurnEnd();
        }
        else
            TurnEnd();
    }

    public override void TurnEnd()
    {
        base.TurnEnd();
        _battleManager.ReadyForEnemy();
    }

    //stateConditions����Ʈ���� ��밡�� �� ���¸� �켱������ ���� ã�� �ش� ���� �������� ��ȯ
    private StateEnum SelectState()
    {
        StateEnum stateEnum = 0;//����� ���� �����Լ� �̸�

        //������ ��Ÿ� ����ϱ�
        int distance = 99;//�÷��̾���� �Ÿ�
        int thisIndex = _battleManager.GetPlatformIndexForObj(gameObject);//���� ��ġ �� ��������
        int countDistance = 0;//�÷��̾� ���� �Ÿ��� ī��Ʈ�� ���� ����Ǵ� ����
        //�÷��̾� ���� �Ÿ� ��������
        for (int i = thisIndex + (Direction == CharacterDirection.Right ? 1: -1);
            Direction == CharacterDirection.Right ? i < _battleManager.PlatformList.Length : i >= 0;
            i += (Direction == CharacterDirection.Right ? 1 : -1))
        {
            
            countDistance++;
            GameObject targetObj = _battleManager.GetOnPlatformObj(i);
            if(targetObj != null)
            {
                //��λ� �÷��̾ ������ �Ÿ� ����
                if (targetObj.layer == LayerMask.NameToLayer("Player"))
                {
                    distance = countDistance;
                    break;
                }
            }
        }

        //�켱 ������ ���� �� �ൿ ����
        foreach (StateCondition condition in stateConditions)
        {
            //��ų ��� ���� ���� üũ
            if (condition.nowCoolTime == 0 && condition.range >= distance)
            {
                condition.nowCoolTime = condition.cooldown;//��Ÿ�� ����
                Debug.Log(condition.stateEnum);
                //�غ��ؾ��ϴ� ��ų������ ���� ���� ó��
                if (condition.delayTurn > 0)
                    TransitionState(StateEnum.EnemyReadyToState, condition.stateEnum, condition.delayTurn);//���� ����
                else
                    TransitionState(condition.stateEnum);//���� ����
                break;
            }
        }

        return stateEnum;
    }

    private void OnDestroy()
    {
        //�� ���� ���� �� dropMoney��ŭ �������� ���� ����
        _battleManager.stageRewards += dropMoney;
    }

    private void DirectionSetting()
    {
        //�� ���� �� �÷��̾ �ٶ󺸴� �������� ��ȯ ��Ű��
        float targetPos = _battleManager.onPlayer.gameObject.transform.position.x;//�÷��̾� ��ġ ��������
        float thisPos = gameObject.transform.position.x;//�ش� ��ü�� ��ġ ��������
        Direction = targetPos < thisPos ? CharacterDirection.Left : CharacterDirection.Right;
    }


}
