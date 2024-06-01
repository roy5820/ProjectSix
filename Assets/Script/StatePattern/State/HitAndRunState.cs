using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAndRunState : MoveState
{
    public string dashAniParamater = "";//대쉬 애니메이션
    public float dashDelay = 0.1f;//백대쉬 실행 전 딜레이
    public float powerCoefficient = 1.2f;//공격력 계수
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

        //캐릭터 도주 방향 설정 
        CharacterDirection runDirection = 
            (characterController.Direction == CharacterDirection.Left ? CharacterDirection.Right : CharacterDirection.Left);
        _animator.SetBool("IsFront", false);//애니메이션 이동 방향 설정
        _animator.SetTrigger(dashAniParamater);//백대쉬 애니메이션 재생
        yield return new WaitForSeconds(dashDelay);//대쉬 딜레이
        yield return base.StateFuntion(runDirection);//공격 실행
    }
}
