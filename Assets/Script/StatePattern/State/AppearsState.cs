using System.Collections;
using UnityEngine;

public class AppearsState : StateBase
{
    protected override IEnumerator StateFuntion(params object[] datas)
    {
        //ĳ���� ���� �� �ִϸ��̼� ��� ����
        Debug.Log(gameObject.name + " is Appears");
        yield return null;
    }
}