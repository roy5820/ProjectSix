using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighEfficiencyEnergyCheep : CheepBase
{
    public int increasedRecoveryAmount = 1;//���� ȸ����
    public int maximumReductionAmount = 5;//���͸� �ִ�ġ ���ҷ�
    //��ȿ�� ������ Ĩ
    //���͸� ȸ���� ���� �� �ִ� ���͸��� ����
    public override void ActivateChipEffect()
    {
        base.ActivateChipEffect();
        PlayerController _playerController = (PlayerController)_characterController;
        Debug.Log(_playerController);
        //���͸� ȸ���� ���� �� �ִ� ���� ����
        if (_playerController != null)
        {
            Debug.Log("��ȿ�� ���͸� ����");
            _playerController.batteryRecoveryFigures += increasedRecoveryAmount;
            _playerController._characterStatus.maxBattery = maximumReductionAmount;
        }
    }
}
