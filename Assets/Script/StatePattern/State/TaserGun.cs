using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//캐릭터 일반공격
public class TaserGun : StateBase
{
    public float powerCoefficient = 1.0f;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//계산할 데미지 구하기

        CharacterDirection characterDir = characterController.direction;//캐릭터 방향가져오기

        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}
