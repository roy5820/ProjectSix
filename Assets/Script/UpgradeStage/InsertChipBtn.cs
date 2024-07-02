using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//칩 삽입 버튼 구현
public class InsertChipBtn : ChipBtnBase
{
    public AudioClip chipSound = null;
    //칩삽입 이벤트 구현 부분
    public override void OnChipEvent()
    {
        //삽입 자리 탐색
        int insertIndex = _gameManager.cheepInventory.FindIndex(chip => chip.Equals(-1));
        
        //삽입할 자리가 없으면 리턴
        if (insertIndex == -1)
            return;

        _gameManager.cheepInventory[insertIndex] = chipInfo.CheepID;//칩 장비
        if (chipSound)
            SoundManger.Instance.PlaySFX(chipSound);
        base.OnChipEvent();
    }
}
