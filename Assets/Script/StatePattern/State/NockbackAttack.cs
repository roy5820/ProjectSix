using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//캐릭터 일반공격
public class NockbackAttack : StateBase
{
    public float powerCoefficient = 1.0f;
    public float nockBackPower = 100f;

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //공격 구현
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//계산할 데미지 구하기

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

            //넉백 구현
            GameObject targetObj = _battleManager.GetOnPlatformObj(attackIndex);

            if(targetObj != null)
            {
                CharacterMovement movement = targetObj.GetComponent<CharacterMovement>();//캐릭터 무브먼트 가져오기
                CharacterDirection moveDirection = characterController.direction;//입력 받은 캐릭터 이동방향 설정
                movement.moveCoroutine = StartCoroutine(movement.StraightLineMovement((int)moveDirection, nockBackPower, 1));//캐릭터 무브먼트를 사용하여 이동 구현

                //이동중 턴 종료 방지
                while (movement.moveCoroutine != null)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(sateDelayTime);
            }
        }

        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}
