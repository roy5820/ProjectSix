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