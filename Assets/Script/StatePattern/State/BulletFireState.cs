using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//캐릭터 일반공격
public class BulletFireState : StateBase
{
    public float powerCoefficient = 1.0f;
    public GameObject bulletPre;//발사할 총알 오브젝트
    public Transform firePoint;//발사 위치
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        int thisDamage = (int)(characterController._characterStatus.offensePower * powerCoefficient);//계산할 데미지 구하기

        CharacterDirection characterDir = characterController.direction;//바라보는 방향가져오기

        GameObject bullet = Instantiate(bulletPre, firePoint.position, Quaternion.identity);//총알 오브젝트 생성
        BulletBase bulletBase = bullet.GetComponent<BulletBase>();//총알 컴포넌트가져오기
        bulletBase.fireDir = characterDir;//발사 방향 설정
        bulletBase.damage = thisDamage;//데미지 설정
        bulletBase.OnFire();//총알 발사

        //총알이 발사되어 파괴 될때까지 대기
        while (bullet != null)
        {
            yield return null;
        }

        characterController.TurnEnd();//상태 종료 시 턴 종료
        yield return base.StateFuntion(datas);
    }
}
