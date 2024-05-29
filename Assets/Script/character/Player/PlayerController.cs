using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public int nowBattery = 0;
    public int NowBattery
    {
        get
        {
            return nowBattery;
        }

        set
        {
            nowBattery = value;
            if(nowBattery > _characterStatus.maxBattery)
                nowBattery = _characterStatus.maxBattery;
        }
    }
    public int batteryRecoveryFigures = 1;//���͸� ȸ�� ��
    //�̺�Ʈ ���
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, TurnStart);
    }

    //�̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, TurnStart);
    }
    protected override void Start()
    {
        base.Start();
        //���°� �ʱ�ȭ
        nowBattery = _characterStatus.maxBattery / 2;
    }

    //�� ���� ó��
    public override void TurnStart()
    {
        base.TurnStart();
        //��Ʋ�޴����� onPlayer���� ������ �� �ʱ�ȭ
        if (_battleManager.onPlayer == null)
        {
            _battleManager.onPlayer = this;
        }
            
        isAvailabilityOfAction = true;
        NowBattery += batteryRecoveryFigures;
    }

    public override void StatusValueSetting()
    {
        //���� �޴����� ����� �÷��̾� �������ͽ� ���� ������
        if(gameManager.playerStatus != null)
        {
            _characterStatus = ScriptableObject.CreateInstance<CharacterStatus>();
            _characterStatus.maxHp = gameManager.playerStatus.maxHp;
            _characterStatus.nowHp = gameManager.playerStatus.nowHp;
            _characterStatus.maxBattery = _characterStatusOriginal.maxBattery;
            _characterStatus.offensePower = gameManager.playerStatus.offensePower;

            return;
        }

        base.StatusValueSetting();
    }

    private void OnDestroy()
    {
        TurnEventBus.Publish(TurnEventType.Lose);
    }
}