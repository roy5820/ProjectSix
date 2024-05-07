using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public KeyCode reseKey;

    public List<ItemInfo> itemDB;//아이템 DB
    public List<ItemInfo> playerItemDB;//플레이어가 보유한 아이템 DB

    public int moneyHeld { get; set; }//보유 돈

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        //이벤트 발생 시 호출할 함수 설정
        GameFlowEventBus.Subscribe(GameFlowType.Start, GameStart);
        GameFlowEventBus.Subscribe(GameFlowType.Rest, GameRest);
        GameFlowEventBus.Subscribe(GameFlowType.Lose, GameLose);
        GameFlowEventBus.Subscribe(GameFlowType.Win, GameWin);
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        //이벤트 제거
        GameFlowEventBus.Unsubscribe(GameFlowType.Start, GameStart);
        GameFlowEventBus.Unsubscribe(GameFlowType.Rest, GameRest);
        GameFlowEventBus.Unsubscribe(GameFlowType.Lose, GameLose);
        GameFlowEventBus.Unsubscribe(GameFlowType.Win, GameWin);
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

    //게임 시작시 이벤트 처리
    public void GameStart()
    {
        
    }

    //업그레이드 단계 이벤트 처리
    public void GameRest()
    {
        
    }

    //게임 패배시 이벤트 처리
    public void GameLose()
    {
        Debug.Log("PlayerLose");
    }

    //게임 승리시 이벤트 처리
    public void GameWin()
    {
        Debug.Log("PlayerWin");
    }
}