using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //스폰 몬스터 정보값 관리를 위한 클래스
    [System.Serializable]
    public class SpawnEnemyInfo
    {
        public GameObject enemyPre;//소환할 몬스터 프리펩
        public int spawnIndex;//소환할 타일 인덱스 값

        //생성자

    }

    //웨이브 정보
    [System.Serializable]
    public class WaveInfo
    {
        public int thisTurn;//해당 웨이브가 시작되는 턴
        public List<SpawnEnemyInfo> enemyInfoList;//소환할 Enemy들의 정보값을 가지는 배열
    }

    public List<WaveInfo> waveList;//현재 웨이브 정보를 가지는 리스트 객체

    public TurnState currentState;//현재 턴 상태를 저장하는 변수


    private void Start()
    {
        currentState = TurnState.TurnStart;//현재 TurnStart상태로 초기화
    }

    private void Update()
    {
        switch (currentState)
        {
            case TurnState.TurnStart:
                // 턴 시작 시 필요한 로직을 여기에 작성

                currentState = TurnState.PlayerTurn;
                break;
            case TurnState.PlayerTurn:
                // 플레이어 턴 동안 필요한 로직을 여기에 작성
                
                currentState = TurnState.EnemyTurn;
                break;
            case TurnState.EnemyTurn:
                // 적 턴 동안 필요한 로직을 여기에 작성

                currentState = TurnState.TurnEnd;
                break;
            case TurnState.TurnEnd:
                // 턴 종료 시 필요한 로직을 여기에 작성

                currentState = TurnState.TurnStart;
                break;
        }
    }
}
