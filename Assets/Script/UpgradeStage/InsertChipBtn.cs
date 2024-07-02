using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ĩ ���� ��ư ����
public class InsertChipBtn : ChipBtnBase
{
    public AudioClip chipSound = null;
    //Ĩ���� �̺�Ʈ ���� �κ�
    public override void OnChipEvent()
    {
        //���� �ڸ� Ž��
        int insertIndex = _gameManager.cheepInventory.FindIndex(chip => chip.Equals(-1));
        
        //������ �ڸ��� ������ ����
        if (insertIndex == -1)
            return;

        _gameManager.cheepInventory[insertIndex] = chipInfo.CheepID;//Ĩ ���
        if (chipSound)
            SoundManger.Instance.PlaySFX(chipSound);
        base.OnChipEvent();
    }
}
