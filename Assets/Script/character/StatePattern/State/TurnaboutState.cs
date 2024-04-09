using System.Collections;
using UnityEngine;

public class TurnaboutState : StateBase
{
    protected override IEnumerator StateFuntion()
    {
        //방향 전환 기능 구현
        Vector3 thisLocalScale = this.transform.localScale;
        this.transform.localScale = new Vector3(thisLocalScale.x * -1, thisLocalScale.y, thisLocalScale.z);//로컬 스케일에 x값에 -1을 곱하여 방향 전환 구현
        yield return base.StateFuntion();
    }
}

