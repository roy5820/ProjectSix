using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public KeyCode reseKey;

    public List<int> stageLevel;//게임 진행도 별 스테이지 난이도 관리
    public int nowProgress = 0;//현재 진행도
    public List<ItemInfo> itemDB;//아이템 DB
    public List<ItemInfo> playerItemDB;//플레이어가 보유한 아이템 DB

    public int moneyHeld { get; set; }//보유 돈
    public CharacterStatus playerStatus = null;//플레이어 스테이터스

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.StageClear, StageClear);//TurnStart 이벤트 설정
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.StageClear, StageClear);//TurnStart 이벤트 제거
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(reseKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //스테이지 클리어 이벤트 처리
    public void StageClear()
    {
        //스테이지 클리어시 캐릭터 스테이터스 값 저장하기
        PlayerController player = BattleManager.Instance.onPlayer;
        if (player != null)
        {
            playerStatus = player._characterStatus;
        }

        nowProgress++;//진행도 증가
    }
}