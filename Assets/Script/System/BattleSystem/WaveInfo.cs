using System.Collections;
using System.Collections.Generic;

//웨이브 정보
[System.Serializable]
public class WaveInfo
{
    public int thisTurn;//해당 웨이브가 시작되는 턴
    public List<SpawnEnemyInfo> enemyInfoList;//소환할 Enemy들의 정보값을 가지는 배열
}
