using System.Collections;
using UnityEngine;

//캐릭터 죽음
public class DieState : StateBase
{
    public GameObject effectList = null;//이펙트 들이 생성되는 곳
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //이펙트 비활성화
        if(effectList)
            effectList.SetActive(false);
        yield return new WaitForSeconds(sateDelayTime);//애니메이션 출력을 위한 딜레이
        //캐릭터 죽음 기능 구현
        Debug.Log(gameObject.name + " is Die");
        _battleManager.onEnemysList.Remove(characterController.gameObject);
        Destroy(characterController.gameObject);

        yield return null;
    }
}