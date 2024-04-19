using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public GameManager gameManager;//게임 메니저

    //전투스테이지의 몬스터 스폰 관리를 위한 변수 선언
    public List<StageInfo> stageList;//스테이지 정보를 담은 리스트
    public List<WaveInfo> nowStage;//현제 스테이지 정보를 담을 리스트
    public List<GameObject> onEnemysList = new List<GameObject>();//현재 필드위에 몬스터 리스트
    public PlayerController onPlayer;//필드위에 플레이어 오브젝트
    private int nextWaveNum = 0;//현제 웨이브 num

    private TurnEventType turnState;//현제 턴상태
    private int nowTurnCnt = 0;//경과 턴

    //enemyTurn 관리를 위한 변수 선언
    private bool isEnemyTurn = false;
    private int turnOverEnemyCnt = 0;//턴종료된 몬스터 수
    

    //활성화시 이벤트 설정
    private void OnEnable()
    {
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
    private void Reset()
    {
        
    }

    private void Update()
    {
        //적턴 턴 종료 처리를 위한 부분
        //플레이어 행동 여부에 따라 EnemyTurn이벤트 발생
        if(turnState == TurnEventType.PlayerTurn && onPlayer != null)
        {
            //적 상태처리 완료 여부 카운트하는 부분
            int readyForEnemyCnt = 0;
            foreach(GameObject enemy in onEnemysList)
            {
                EnemyController eContoller = enemy.GetComponent<EnemyController>();
                if (!eContoller.isStatusProcessing)
                    readyForEnemyCnt++;
            }
            Debug.Log("준비된 적 수: "+readyForEnemyCnt);
            //턴 전환 여부 체크
            if(!onPlayer.isTurnReady && !onPlayer.isStatusProcessing && onEnemysList.Count == readyForEnemyCnt)
                TurnEventBus.Publish(TurnEventType.EnemyTurn);//턴 전환 이벤트 발생
        }

        //적 행동 여부에 따라 TurnEnd이벤트 발생
        if (turnState == TurnEventType.EnemyTurn && onPlayer != null)
        {

            //적 상태처리 완료 여부 카운트하는 부분
            int readyForEnemyCnt = 0;
            foreach (GameObject enemy in onEnemysList)
            {
                EnemyController eContoller = enemy.GetComponent<EnemyController>();
                if (!eContoller.isStatusProcessing && !eContoller.isTurnReady)
                    readyForEnemyCnt++;
                Debug.Log(eContoller.isStatusProcessing + ", " + eContoller.isTurnReady);
            }
            Debug.Log("준비된 적 수: " + readyForEnemyCnt);
            //턴 전환 여부 체크
            if (!onPlayer.isStatusProcessing && onEnemysList.Count == readyForEnemyCnt)
                TurnEventBus.Publish(TurnEventType.EnemyTurn);//턴 전환 이벤트 발생
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
        if(gameManager == null)
            gameManager = GameManager.Instance;
        //전투 시작 시 스테이지 정보 초기화 작업
        nowTurnCnt = 0;
        nextWaveNum = 0;

        TurnEventBus. Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }


    //전투 종료 처리

    //턴 시작 시 이벤트 처리
    public void TurnStart()
    {
        turnState = TurnEventType.TurnStart;
        Debug.Log(turnState);

        //Enemy 스폰 여부 확인
        if (nextWaveNum < nowStage.Count && onEnemysList.Count == 0)
        {
            //스폰할 몬스터 리스트의 몬스터들을 소환
            foreach (SpawnEnemyInfo enemy in nowStage[nextWaveNum].enemyInfoList)
            {
                int platformSIze = gameManager.PlatformList.Length;
                //스폰 위치에 따른 스폰 위치 탐색
                for (int i = (enemy.spawnPos < 0 ? 0 : platformSIze - 1);
                    (enemy.spawnPos < 0 ? i < platformSIze : i >= 0);
                    i += (enemy.spawnPos < 0 ? 1 : -1))
                {
                    //해당 위치에 몬스터 스폰 여부 확인
                    if (gameManager.GetOnPlatformObj(i) == null)
                    {
                        GameObject enemyPre = Instantiate(enemy.enemyPre, gameManager.GetStandingPos(i), Quaternion.identity);
                        onEnemysList.Add(enemyPre);
                        //적 스폰 시 플레이어를 바라보는 방향으로 전환 시키기
                        int targetIndex = gameManager.GetPlatformIndexForObj(GameObject.FindGameObjectWithTag("Player"));//플레이어 위치 가져오기
                        int thisIndex = i;//해당 객체의 위치 가져오기

                        //바로보는 방향에 타겟이 없으면 방향 전환
                        if (targetIndex < thisIndex)
                        {
                            enemyPre.transform.localScale = new Vector3(-1, 1, 1);
                        }
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
        turnState = TurnEventType.PlayerTurn;
        Debug.Log(turnState);
    }


    //적턴 시작 시 이벤트 처리
    public void EnemyTurn()
    {
        isEnemyTurn = true;//적턴 여부 활성화
        turnState = TurnEventType.EnemyTurn;
        turnOverEnemyCnt = 0;//적턴 시작 시 턴오버 카운트 0으로 초기화
        Debug.Log(turnState);
    }

    //턴 종료 시 이벤트 처리
    public void TurnEnd()
    {
        isEnemyTurn = false;//적턴 여부 비활성화
        turnState = TurnEventType.TurnEnd;

        nowTurnCnt++;//턴 종료 시 경과 턴 +1
        TurnEventBus.Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "TURN STATUS: " + turnState);
    }
}
