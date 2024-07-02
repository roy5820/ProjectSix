using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//보유 칩 버튼 구현
public class HeldChipBtn : ChipBtnBase
{
    public int chipIndex = -1;//수정할 칩 인덱스 값
    public AudioClip chipSound = null;

    protected override void Update()
    {
        //칩정보 갱신
        chipInfo = _gameManager.cheepDataBase.Find(chip => chip.CheepID.Equals(_gameManager.cheepInventory[chipIndex]));

        base.Update();
    }

    //칩 제거 이벤트 구현 부분
    public override void OnChipEvent()
    {
        //null체크
        if (chipInfo == null)
            return;

        //해당 칩을 칩인벤토리에서 제거
        _gameManager.cheepInventory[chipIndex] = -1;
        chipInfo = null;

        if(chipSound)
            SoundManger.Instance.PlaySFX(chipSound);

        base.OnChipEvent();
    }
}
