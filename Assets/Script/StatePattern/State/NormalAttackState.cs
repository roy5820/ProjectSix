using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//캐릭터 일반공격
public class NormalAttackState : StateBase
{
    public float powerCoefficient = 1.0f;
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        //공격 계수 인자 값이 있으면 값 적용
        if (datas.Length > 0)
            powerCoefficient = (float)datas[0];
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//계산할 데미지 구하기

        CharacterDirection characterDir = characterController.Direction;//캐릭터 방향가져오기

        int onIndex = _battleManager.GetPlatformIndexForObj(characterController.gameObject);
        int attackIndex = onIndex + ((int)characterDir);//공격 할 플렛폼 index

        //공격 가능 여부
        if (attackIndex >= 0 && attackIndex < _battleManager.PlatformList.Length)
        {
            //데미지 계산 부분
            _battleManager.GiveDamage(attackIndex, thisDamage);
        }

        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}
