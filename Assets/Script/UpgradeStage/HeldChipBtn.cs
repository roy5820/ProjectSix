using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���� Ĩ ��ư ����
public class HeldChipBtn : ChipBtnBase
{
    public int chipIndex = -1;//������ Ĩ �ε��� ��
    public AudioClip chipSound = null;

    protected override void Update()
    {
        //Ĩ���� ����
        chipInfo = _gameManager.cheepDataBase.Find(chip => chip.CheepID.Equals(_gameManager.cheepInventory[chipIndex]));

        base.Update();
    }

    //Ĩ ���� �̺�Ʈ ���� �κ�
    public override void OnChipEvent()
    {
        //nullüũ
        if (chipInfo == null)
            return;

        //�ش� Ĩ�� Ĩ�κ��丮���� ����
        _gameManager.cheepInventory[chipIndex] = -1;
        chipInfo = null;

        if(chipSound)
            SoundManger.Instance.PlaySFX(chipSound);

        base.OnChipEvent();
    }
}
