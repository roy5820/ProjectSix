using System.Collections;
using UnityEngine;

//스폰 몬스터 정보값 관리를 위한 클래스
[System.Serializable]
public class SpawnEnemyInfo
{
    public GameObject enemyPre;//소환할 몬스터 프리펩
    public int spawnIndex;//소환할 타일 인덱스 값

    //생성자
    public SpawnEnemyInfo(GameObject enemyPre, int spawnIndex)
    {
        this.enemyPre = enemyPre;
        this.spawnIndex = spawnIndex;
    }
}
