using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Rendering.CameraUI;

public class GameManager : Singleton<GameManager>
{
    public KeyCode reseKey;

    public List<int> stageLevel;//게임 진행도 별 스테이지 난이도 관리
    public int nowProgress = 0;//현재 진행도
    public List<ItemData> itemDB;//아이템 DB
    public List<PlayerHaveItemData> playerItemDB;//플레이어가 보유한 아이템 DB

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

    //아이템 데이터 베이스 관련 함수들
    //미 보유 아이템을 List<ItemInfo> 형식으로 출력하는 함수 (number: 출력할 아이템 수)
    public List<ItemInfo> RandomOutputOfUnownedItems(int number)
    {
        List<ItemInfo> outputList = new List<ItemInfo>();

        

        return outputList;
    }

    //보유 아이템을 모두 List<itemInfo> 형식으로 출력하는 함수
    public List<ItemInfo> OutputOfHaveItems()
    {
        List<ItemInfo> outputList = new List<ItemInfo>();

        //playerItemDB의 정보를 토대로 itemDB에 아이템 정보를 가져와 outputList를 체우는 반복문
        for (int i = 0; i < playerItemDB.Count; i++)
        {
            //플레이어 아이템 DB의 아이템 명과 레벨을 통해서 아이템 정보 가져와 outputList에 추가
            ItemData getItem = itemDB.Find(item => item.itemName.Equals(playerItemDB[i].itemName));
            outputList.Add(getItem.itemInfo[playerItemDB[i].itemLevel]);
        }

        return outputList;
    }
}