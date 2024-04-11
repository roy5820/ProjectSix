using System.Collections;
using UnityEngine;

public class DieState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //캐릭터 죽음 기능 구현
        Debug.Log(gameObject.name + " is Die");
        _gameManager.GetComponent<BattleManager>().onEnemysList.Remove(gameObject);
        Destroy(gameObject);
        yield return null;
    }
}

