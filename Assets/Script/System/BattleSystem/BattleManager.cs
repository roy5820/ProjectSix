using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    GameManager gameManager;//게임 메니저

    //전투스테이지의 몬스터 스폰 관리를 위한 변수 선언
    public List<StageInfo> stageList;//스테이지 정보를 담은 리스트
    public List<WaveInfo> nowStage;//현제 스테이지 정보를 담을 리스트
    public List<GameObject> onEnemysList = new List<GameObject>();//현재 필드위에 몬스터 리스트
    private int nextWaveNum = 0;//현제 웨이브 num

    private string turnState;//현제 턴상태
    private int nowTurnCnt = 0;//경과 턴

    //enemyTurn 관리를 위한 변수 선언
    private bool isEnemyTurn = false;
    private int turnOverEnemyCnt = 0;//턴종료된 몬스터 수
    

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        gameManager = GameManager.Instance;//게임 메니저 값 초기화

        GameFlowEventBus.Subscribe(GameFlowType.Battle, BattleStart);//Battle 이벤트 설정

        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        GameFlowEventBus.Unsubscribe(GameFlowType.Battle, BattleStart);//Battle 이벤트 제거

        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 제거
    }

    private void Update()
    {
        //턴종료 수가 필드의 몬스터 수와 같을 경우 턴종료 이벤트 발생
        if (isEnemyTurn && (turnOverEnemyCnt >= onEnemysList.Count))
        {
            Debug.Log("적 행동 완료");
            TurnEventBus.Publish(TurnEventType.TurnEnd);//턴 종료 이벤트 발생
        }
    }

    //Enemy 턴 종료 체크를 위한 프로퍼티
    public int OnTurnOverEnemyCnt
    {
        get
        {
            return turnOverEnemyCnt;
        }
        set
        {
            turnOverEnemyCnt = value;
        }
    }

    //배틀 시작 시 이벤트 처리
    public void BattleStart()
    {
        //전투 시작 시 스테이지 정보 초기화 작업
        nowTurnCnt = 0;
        nextWaveNum = 0;

        TurnEventBus. Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }

    //전투 종료 처리

    //턴 시작 시 이벤트 처리
    public void TurnStart()
    {
        turnState = "TurnStart";
        Debug.Log(turnState);

        //Enemy 스폰 여부 확인
        if (nextWaveNum < nowStage.Count && onEnemysList.Count == 0)
        {
            //스폰할 몬스터 리스트의 몬스터들을 소환
            foreach (SpawnEnemyInfo enemy in nowStage[nextWaveNum].enemyInfoList)
            {
                int platformSIze = gameManager.PlatformList.Length;
                //스폰 위치에 따른 스폰 위치 탐색
                for (int i = (enemy.spawnPos < 0 ? 0 : platformSIze - 1); (enemy.spawnPos < 0 ? i < platformSIze : i >= 0); i += (enemy.spawnPos < 0 ? 1 : -1))
                {
                    //해당 위치에 몬스터 스폰 여부 확인
                    if (gameManager.GetOnPlatformObj(i) == null)
                    {
                        GameObject enemyPre = Instantiate(enemy.enemyPre, gameManager.GetStandingPos(i), Quaternion.identity);//적 프리펩 생성
                        Debug.Log(enemyPre.GetComponent<EnemyController>());
                        onEnemysList.Add(enemyPre);//적 리스트에 추가
                        break;
                    }
                }
            }
            nextWaveNum++;//다음에 스폰할 웨이브 번호 1 증가
        }
        
        TurnEventBus.Publish(TurnEventType.PlayerTurn);//PlayerTurn 이벤트 발생
    }

    //플레이어 턴 시작 시 이벤트 처리
    public void PlayerTurn()
    {
        turnState = "PlayerTurn";
        Debug.Log(turnState);
    }


    //적턴 시작 시 이벤트 처리
    public void EnemyTurn()
    {
        isEnemyTurn = true;//적턴 여부 활성화
        turnState = "EnemyTurn";
        turnOverEnemyCnt = 0;//적턴 시작 시 턴오버 카운트 0으로 초기화
        Debug.Log(turnState);
    }

    //턴 종료 시 이벤트 처리
    public void TurnEnd()
    {
        isEnemyTurn = false;//적턴 여부 비활성화
        turnState = "TurnEnd";
        Debug.Log(turnState);
        nowTurnCnt++;//턴 종료 시 경과 턴 +1
        TurnEventBus.Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "TURN STATUS: " + turnState);
    }
}
