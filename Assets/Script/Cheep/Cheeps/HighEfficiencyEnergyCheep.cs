using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighEfficiencyEnergyCheep : CheepBase
{
    public int increasedRecoveryAmount = 1;//증가 회복량
    public int maximumReductionAmount = 5;//배터리 최대치 감소량
    //고효율 에너지 칩
    //베터리 회복량 증가 및 최대 베터리량 감소
    public override void ActivateChipEffect()
    {
        PlayerController _playerController = (PlayerController)_characterController;
        //배터리 회복량 증가 및 최대 개수 감소
        if(_playerController != null)
        {
            _playerController.batteryRecoveryFigures += increasedRecoveryAmount;
            _playerController._characterStatus.maxBattery = maximumReductionAmount;
        }
    }
}
