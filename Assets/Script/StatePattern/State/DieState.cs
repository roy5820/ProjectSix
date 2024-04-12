using System.Collections;
using UnityEngine;

public class DieState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //ĳ���� ���� ��� ����
        Debug.Log(gameObject.name + " is Die");
        _gameManager.GetComponent<BattleManager>().onEnemysList.Remove(gameObject);
        Destroy(gameObject);
        yield return null;
    }
}
