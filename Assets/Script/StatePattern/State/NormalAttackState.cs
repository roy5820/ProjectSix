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
        int thisDamage = (int)(characterController.offensePower * powerCoefficient);//계산할 데미지 구하기

        CharacterDirection characterDir = characterController.direction;//캐릭터 방향가져오기

        int onIndex = _battleManager.GetPlatformIndexForObj(characterController.gameObject);
        int attackIndex = onIndex + ((int)characterDir);//공격 할 플렛폼 index

        //공격 가능 여부
        if (attackIndex >= 0 && attackIndex < _battleManager.PlatformList.Length)
        {
            //애니메이션 처리 부분
            Debug.Log(attackIndex + ", " + attackIndex);
            //데미지 계산 부분
            _battleManager.GiveDamage(attackIndex, thisDamage);
        }

        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}
