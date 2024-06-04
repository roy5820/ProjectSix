using System.Collections;
using UnityEngine;
//캐릭터 피격
public class HitState : StateBase
{
    private int hitDamage { get; set; }//피격 시 계산할 데미지
    public bool cameraShake = false;//화면 흔들림 여부
    public float shakeTime = 0.3f;//흔들림 지속 시간
    public float shakePower = 20f;//흔들림 파워

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //체력이 0 이상일 때 피격 처리
        if (characterController.NowHp > 0)
        {
            hitDamage = (int)datas[0];
            Debug.Log("Hit: " + hitDamage);
            //피격 처리 기능 구현
            characterController.NowHp -= hitDamage;//데미지 계산
            Debug.Log(gameObject.name + " is Hit, nowDamage: " + hitDamage + " nowHp: " + characterController.NowHp);

            //화면 흔들림 구현
            if (cameraShake)
                CameraController.Instance.OnShake(shakeTime, shakePower);

            yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이

            //체력이 0이면 죽음 처리
            if (characterController.NowHp == 0)
            {
                characterController.isAvailabilityOfAction = false;
                characterController.TransitionState(StateEnum.Die);
                yield break;
            }
        }
        

        yield return base.StateFuntion(datas);
    }
}

