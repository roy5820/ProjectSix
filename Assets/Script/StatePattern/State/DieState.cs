using System.Collections;
using UnityEngine;
//캐릭터 죽음
public class DieState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        //캐릭터 죽음 기능 구현
        Debug.Log(gameObject.name + " is Die");
        BattleManager.Instance.onEnemysList.Remove(gameObject);
        Destroy(gameObject);
        yield return null;
    }
}

