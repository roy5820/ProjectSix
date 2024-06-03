using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private GameManager gameManager;//게임 메니저

    public GameObject[] PlatformList;//플랫폼 리스트
    //전투스테이지의 몬스터 스폰 관리를 위한 변수 선언
    public List<StageInfo> stageList;//스테이지 정보를 담은 리스트
    public StageInfo nowStage;//현제 스테이지 정보를 담을 리스트

    public List<GameObject> onEnemysList = new List<GameObject>();//현재 필드위에 몬스터 리스트
    public PlayerController onPlayer;//필드위에 플레이어 오브젝트
    private int nextWaveNum = 0;//현제 웨이브 num

    private TurnEventType turnState;//현제 턴상태
    public int nowTurnCnt = 0;//경과 턴

    public int stageRewards {get;set;}//스테이지 보상
    private int readyForEnemyCnt = 0;//준비된 적 수
    //활성화시 이벤트 설정
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd 이벤트 제거
    }

    private void Start()
    {
        BattleStart();
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

            //턴 전환 여부 체크
            if((!onPlayer.isTurnReady && !onPlayer.isStatusProcessing && onEnemysList.Count == readyForEnemyCnt)
                || (onEnemysList.Count == 0 && !onPlayer.isStatusProcessing))
            {
                TurnEventBus.Publish(TurnEventType.EnemyTurn);//턴 전환 이벤트 발생
            }
                
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
                {
                    readyForEnemyCnt++;
                }
            }

            //턴 전환 여부 체크
            if (!onPlayer.isStatusProcessing && onEnemysList.Count <= readyForEnemyCnt)
            {
                TurnEventBus.Publish(TurnEventType.TurnEnd);//턴 전환 이벤트 발생
            }
        }
    }

    //이벤트 처리 관련 부분
    //배틀 시작 시 이벤트 처리
    public void BattleStart()
    {
        if(gameManager == null)
            gameManager = GameManager.Instance;

        //스테이지 정보 갱신
        List<StageInfo> filteredStages =
            stageList.Where(stage => stage.stageLevel == gameManager.stageLevel[gameManager.nowProgress]).ToList();

        if (filteredStages.Count > 0)
        {
            int ranIndex = Random.Range(0, filteredStages.Count);
            nowStage = filteredStages[ranIndex];
        }

        //전투 시작 시 스테이지 정보 초기화 작업
        nowTurnCnt = 0;
        nextWaveNum = 0;
        readyForEnemyCnt = 0;
        TurnEventBus. Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
    }


    //턴 시작 시 이벤트 처리
    public void TurnStart()
    {
        turnState = TurnEventType.TurnStart;
        Debug.Log(turnState);

        //Enemy 스폰 여부 확인
        if (nextWaveNum < nowStage.waveList.Count && onEnemysList.Count == 0)
        {
            //스폰할 몬스터 리스트의 몬스터들을 소환
            foreach (SpawnEnemyInfo enemy in nowStage.waveList[nextWaveNum].enemyInfoList)
            {
                int platformSIze = PlatformList.Length;
                //스폰 위치에 따른 스폰 위치 탐색
                for (int i = (enemy.spawnPos < 0 ? 0 : platformSIze - 1);
                    (enemy.spawnPos < 0 ? i < platformSIze : i >= 0);
                    i += (enemy.spawnPos < 0 ? 1 : -1))
                {
                    //해당 위치에 몬스터 스폰 여부 확인
                    if (GetOnPlatformObj(i) == null)
                    {
                        GameObject enemyPre = Instantiate(enemy.enemyPre, GetStandingPos(i), Quaternion.identity);
                        onEnemysList.Add(enemyPre);
                        
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
        turnState = TurnEventType.EnemyTurn;
        Debug.Log(turnState);
    }

    //턴 종료 시 이벤트 처리
    public void TurnEnd()
    {
        turnState = TurnEventType.TurnEnd;
        //스테이지 클리어 체크 후 스테이지 클리어 이벤트 발생
        if (onEnemysList.Count == 0 && nowStage.waveList.Count <= nextWaveNum) {
            Debug.Log("Clear");
            if(gameManager.nowProgress+1 >= gameManager.stageLevel.Count)
                TurnEventBus.Publish(TurnEventType.Win);
            else
                TurnEventBus.Publish(TurnEventType.StageClear);
        }
        else
        {
            nowTurnCnt++;//턴 종료 시 경과 턴 +1
            
            TurnEventBus.Publish(TurnEventType.TurnStart);//TurnStart 이벤트 발생
        }
    }

    //전투 스테이지 시 사용할 기능들
    //특정 오브젝트가 속한 플렛폼 검색하여 index 번호를 반환하는 함수. null일 시 -1로 리턴
    public int GetPlatformIndexForObj(GameObject chracterObj)
    {
        int index = -1;

        //반복문으로 입력받은 게임 오브젝트가 속한 플렛폼 순차 탐색
        for (int i = 0; i < PlatformList.Length; i++)
        {
            if (PlatformList[i].GetComponent<PlatformInfoManagement>().OnPlatformCharacter == chracterObj)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    //특정 위치의 플렛폼안에 캐릭터obj의 정보 반환 함수
    public GameObject GetOnPlatformObj(int indexNum)
    {
        GameObject returnObj = null;//반환할 오브젝트 값

        //플렛폼 리스트의 유효한 인덱스 값인지 체크
        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnObj = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().OnPlatformCharacter;
        }
        return returnObj;
    }

    //특정 위치의 플렛폼안에  정보 반환 함수
    public Vector3 GetStandingPos(int indexNum)
    {
        Vector3 returnPos = Vector3.zero;//반환할 위치 값

        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnPos = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().StandingPos;
        }

        return returnPos;
    }

    //특정 위치 타일에 적에게 데미지 부여
    public void GiveDamage(int index, int damage)
    {
        GameObject tartget = GetOnPlatformObj(index);//데미지를 줄 오브젝트 가져오기

        //null체크
        if ((tartget))
        {
            tartget.GetComponent<CharacterController>().TransitionState(StateEnum.Hit, damage);//대상 피격 상태 발생
        }
    }

    //적 오브젝트 턴 종료시 readyForEnemyCnt++
    public void ReadyForEnemy()
    {
        readyForEnemyCnt++;
    }
}
