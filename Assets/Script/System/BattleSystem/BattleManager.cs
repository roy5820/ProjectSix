using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private GameManager gameManager;//���� �޴���

    public GameObject[] PlatformList;//�÷��� ����Ʈ
    //�������������� ���� ���� ������ ���� ���� ����
    public List<StageInfo> stageList;//�������� ������ ���� ����Ʈ
    public StageInfo nowStage;//���� �������� ������ ���� ����Ʈ

    public List<GameObject> onEnemysList = new List<GameObject>();//���� �ʵ����� ���� ����Ʈ
    public PlayerController onPlayer;//�ʵ����� �÷��̾� ������Ʈ
    private int nextWaveNum = 0;//���� ���̺� num

    private TurnEventType turnState;//���� �ϻ���
    public int nowTurnCnt = 0;//��� ��

    public int stageRewards {get;set;}//�������� ����
    private int readyForEnemyCnt = 0;//�غ�� �� ��
    //Ȱ��ȭ�� �̺�Ʈ ����
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart �̺�Ʈ ����
        TurnEventBus.Subscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn �̺�Ʈ ����
        TurnEventBus.Subscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn �̺�Ʈ ����
        TurnEventBus.Subscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    //��Ȱ��ȭ�� �̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart �̺�Ʈ ����
        TurnEventBus.Unsubscribe(TurnEventType.PlayerTurn, PlayerTurn);//PlayerTurn �̺�Ʈ ����
        TurnEventBus.Unsubscribe(TurnEventType.EnemyTurn, EnemyTurn);//EnemyTurn �̺�Ʈ ����
        TurnEventBus.Unsubscribe(TurnEventType.TurnEnd, TurnEnd);//TurnEnd �̺�Ʈ ����
    }

    private void Start()
    {
        BattleStart();
    }

    private void Update()
    {
        //���� �� ���� ó���� ���� �κ�
        //�÷��̾� �ൿ ���ο� ���� EnemyTurn�̺�Ʈ �߻�
        if(turnState == TurnEventType.PlayerTurn && onPlayer != null)
        {
            //�� ����ó�� �Ϸ� ���� ī��Ʈ�ϴ� �κ�
            int readyForEnemyCnt = 0;
            foreach(GameObject enemy in onEnemysList)
            {
                EnemyController eContoller = enemy.GetComponent<EnemyController>();
                if (!eContoller.isStatusProcessing)
                    readyForEnemyCnt++;
            }

            //�� ��ȯ ���� üũ
            if((!onPlayer.isTurnReady && !onPlayer.isStatusProcessing && onEnemysList.Count == readyForEnemyCnt)
                || (onEnemysList.Count == 0 && !onPlayer.isStatusProcessing))
            {
                TurnEventBus.Publish(TurnEventType.EnemyTurn);//�� ��ȯ �̺�Ʈ �߻�
            }
                
        }

        //�� �ൿ ���ο� ���� TurnEnd�̺�Ʈ �߻�
        if (turnState == TurnEventType.EnemyTurn && onPlayer != null)
        {
            //�� ����ó�� �Ϸ� ���� ī��Ʈ�ϴ� �κ�
            int readyForEnemyCnt = 0;
            foreach (GameObject enemy in onEnemysList)
            {
                EnemyController eContoller = enemy.GetComponent<EnemyController>();
                if (!eContoller.isStatusProcessing && !eContoller.isTurnReady)
                {
                    readyForEnemyCnt++;
                }
            }

            //�� ��ȯ ���� üũ
            if (!onPlayer.isStatusProcessing && onEnemysList.Count <= readyForEnemyCnt)
            {
                TurnEventBus.Publish(TurnEventType.TurnEnd);//�� ��ȯ �̺�Ʈ �߻�
            }
        }
    }

    //�̺�Ʈ ó�� ���� �κ�
    //��Ʋ ���� �� �̺�Ʈ ó��
    public void BattleStart()
    {
        if(gameManager == null)
            gameManager = GameManager.Instance;

        //�������� ���� ����
        List<StageInfo> filteredStages =
            stageList.Where(stage => stage.stageLevel == gameManager.stageLevel[gameManager.nowProgress]).ToList();

        if (filteredStages.Count > 0)
        {
            int ranIndex = Random.Range(0, filteredStages.Count);
            nowStage = filteredStages[ranIndex];
        }

        //���� ���� �� �������� ���� �ʱ�ȭ �۾�
        nowTurnCnt = 0;
        nextWaveNum = 0;
        readyForEnemyCnt = 0;
        TurnEventBus. Publish(TurnEventType.TurnStart);//TurnStart �̺�Ʈ �߻�
    }


    //�� ���� �� �̺�Ʈ ó��
    public void TurnStart()
    {
        turnState = TurnEventType.TurnStart;
        Debug.Log(turnState);

        //Enemy ���� ���� Ȯ��
        if (nextWaveNum < nowStage.waveList.Count && onEnemysList.Count == 0)
        {
            //������ ���� ����Ʈ�� ���͵��� ��ȯ
            foreach (SpawnEnemyInfo enemy in nowStage.waveList[nextWaveNum].enemyInfoList)
            {
                int platformSIze = PlatformList.Length;
                //���� ��ġ�� ���� ���� ��ġ Ž��
                for (int i = (enemy.spawnPos < 0 ? 0 : platformSIze - 1);
                    (enemy.spawnPos < 0 ? i < platformSIze : i >= 0);
                    i += (enemy.spawnPos < 0 ? 1 : -1))
                {
                    //�ش� ��ġ�� ���� ���� ���� Ȯ��
                    if (GetOnPlatformObj(i) == null)
                    {
                        GameObject enemyPre = Instantiate(enemy.enemyPre, GetStandingPos(i), Quaternion.identity);
                        onEnemysList.Add(enemyPre);
                        
                        break;
                    }
                }
            }
            nextWaveNum++;//������ ������ ���̺� ��ȣ 1 ����
        }
        
        TurnEventBus.Publish(TurnEventType.PlayerTurn);//PlayerTurn �̺�Ʈ �߻�
    }

    //�÷��̾� �� ���� �� �̺�Ʈ ó��
    public void PlayerTurn()
    {
        turnState = TurnEventType.PlayerTurn;
        Debug.Log(turnState);
    }


    //���� ���� �� �̺�Ʈ ó��
    public void EnemyTurn()
    {
        turnState = TurnEventType.EnemyTurn;
        Debug.Log(turnState);
    }

    //�� ���� �� �̺�Ʈ ó��
    public void TurnEnd()
    {
        turnState = TurnEventType.TurnEnd;
        //�������� Ŭ���� üũ �� �������� Ŭ���� �̺�Ʈ �߻�
        if (onEnemysList.Count == 0 && nowStage.waveList.Count <= nextWaveNum) {
            Debug.Log("Clear");
            if(gameManager.nowProgress+1 >= gameManager.stageLevel.Count)
                TurnEventBus.Publish(TurnEventType.Win);
            else
                TurnEventBus.Publish(TurnEventType.StageClear);
        }
        else
        {
            nowTurnCnt++;//�� ���� �� ��� �� +1
            
            TurnEventBus.Publish(TurnEventType.TurnStart);//TurnStart �̺�Ʈ �߻�
        }
    }

    //���� �������� �� ����� ��ɵ�
    //Ư�� ������Ʈ�� ���� �÷��� �˻��Ͽ� index ��ȣ�� ��ȯ�ϴ� �Լ�. null�� �� -1�� ����
    public int GetPlatformIndexForObj(GameObject chracterObj)
    {
        int index = -1;

        //�ݺ������� �Է¹��� ���� ������Ʈ�� ���� �÷��� ���� Ž��
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

    //Ư�� ��ġ�� �÷����ȿ� ĳ����obj�� ���� ��ȯ �Լ�
    public GameObject GetOnPlatformObj(int indexNum)
    {
        GameObject returnObj = null;//��ȯ�� ������Ʈ ��

        //�÷��� ����Ʈ�� ��ȿ�� �ε��� ������ üũ
        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnObj = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().OnPlatformCharacter;
        }
        return returnObj;
    }

    //Ư�� ��ġ�� �÷����ȿ�  ���� ��ȯ �Լ�
    public Vector3 GetStandingPos(int indexNum)
    {
        Vector3 returnPos = Vector3.zero;//��ȯ�� ��ġ ��

        if (indexNum > -1 && indexNum < PlatformList.Length)
        {
            returnPos = PlatformList[indexNum].GetComponent<PlatformInfoManagement>().StandingPos;
        }

        return returnPos;
    }

    //Ư�� ��ġ Ÿ�Ͽ� ������ ������ �ο�
    public void GiveDamage(int index, int damage)
    {
        GameObject tartget = GetOnPlatformObj(index);//�������� �� ������Ʈ ��������

        //nullüũ
        if ((tartget))
        {
            tartget.GetComponent<CharacterController>().TransitionState(StateEnum.Hit, damage);//��� �ǰ� ���� �߻�
        }
    }

    //�� ������Ʈ �� ����� readyForEnemyCnt++
    public void ReadyForEnemy()
    {
        readyForEnemyCnt++;
    }
}
