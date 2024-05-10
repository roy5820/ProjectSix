using System.Collections;
using UnityEngine;
//캐릭터 피격
public class StunState : StateBase
{
    private int hitDamage { get; set; }//피격 시 계산할 데미지
    private bool isStun = false;//스턴 여부
    int stunTurn = 0;//스턴 시간

    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 설정
    }

    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 제거
    }

    protected override IEnumerator StateFuntion(params object[] datas)
    {
        isStun = true;
        characterController.isAvailabilityOfAction = false;//행동불가 상태로 전환
        stunTurn = (int)datas[0];//스턴 시간 설정
        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        //스턴 상태로 전환 후 턴 종료 처리
        characterController.isStatusProcessing = false;
        characterController.TurnEnd();


        while (stunTurn > 0)
        {
            yield return null;
        }

        characterController.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isStun = false;
        characterController.isAvailabilityOfAction = true;//행동가능 상태로 전환
        yield return base.StateFuntion(datas);
    }

    //턴 종료에 따른 스턴 관리
    private void TurnEnd()
    {
        if (isStun)
        {
            Debug.Log("스턴 시간 경과");
            stunTurn--;
        }
            
    }
}

