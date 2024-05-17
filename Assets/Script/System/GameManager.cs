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
    public List<PlayerHaveItemData> playerItemDB;//플레이어 보유 아이템 DB
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
    //미 보유 아이템을 List<ItemData> 형식으로 출력하는 함수 (number: 출력할 아이템 수)
    public List<PlayerHaveItemData> RandomOutputOfUnownedItems(int number)
    {
        List<PlayerHaveItemData> outputList = new List<PlayerHaveItemData>();//리턴할 미보유 아이템 리스트
        List<PlayerHaveItemData> unownedItems = new List<PlayerHaveItemData>();//미보유 아이템 리스트
        //미보유 아이템을 outputList에 추가하는 반복문
        foreach (ItemData data in itemDB)
        {
            PlayerHaveItemData findData = playerItemDB.Find(item => item.itemId.Equals(data.itemId));//플레이어 인벤토리에 현재 아이템이 가져오기
            //추가할 데이터 생성 및 값 초기화
            PlayerHaveItemData addData = new PlayerHaveItemData();
            addData.itemId = data.itemId;
            addData.itemLevel = 0;
            //보유한 아이템 level이 최대인지 체크, 최대인 경우 컨티뉴
            if (findData != null)
            {
                if (findData.itemLevel < data.itemInfo.Count - 1)
                    continue;
                addData.itemLevel = findData.itemLevel + 1;//현재 보유 중인 아이템일 경우 다음 레벨의 아이템으로 설정
            }

            //삽입할 데이터 생성 및 값 초기화


            //미보유 일 경우 outputList에 해당 아이템 삽입
            unownedItems.Add(addData);
        }

        //출력 개수 만큼 outputList리스트에 랜덤으로 요소 추가
        

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
            ItemData getItem = itemDB.Find(item => item.itemId.Equals(playerItemDB[i].itemId));
            outputList.Add(getItem.itemInfo[playerItemDB[i].itemLevel]);
        }
        
        return outputList;
    }
}