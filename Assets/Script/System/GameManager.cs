using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public KeyCode reseKey;

    public List<int> stageLevel;//게임 진행도 별 스테이지 난이도 관리
    public int nowProgress = 0;//현재 진행도
    public List<ItemData> itemDB;//아이템 DB
    public List<PlayerHaveItemData> playerItemDB;//플레이어 보유 아이템 DB
    public int moneyHeld { get; set; }//보유 돈
    public int startMoney = 1000;//시작 보유 돈
    public CharacterStatus playerStatus = null;//플레이어 스테이터스

    //칩기능 관련 전역 변수
    public List<CheepInfo> cheepDataBase;//칩 데이터  베이스
    public List<int> cheepInventory;//칩 인벤토리

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
        moneyHeld = startMoney;//보유돈 시작돈으로 초기화
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
                if (findData.itemLevel >= data.itemInfo.Count - 1)
                    continue;
                addData.itemLevel = findData.itemLevel + 1;//현재 보유 중인 아이템일 경우 다음 레벨의 아이템으로 설정
            }

            addData.itemInfo = data.itemInfo[addData.itemLevel];//아이템 정보값 초기화

            //미보유 일 경우 outputList에 해당 아이템 삽입
            unownedItems.Add(addData);
        }

        //출력 개수 만큼 outputList리스트에 랜덤으로 요소 추가
        while(outputList.Count < number && unownedItems.Count > 0)
        {
            int ranNum = Random.Range(0, unownedItems.Count);//랜덤 숫자 추출
            outputList.Add(unownedItems[ranNum]);//값 추가
            unownedItems.RemoveAt(ranNum);
        }

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

    //플레이어 아이템 획득 구현 함수
    public void PlayerGetItem(int itemID, int itemLV, ItemInfo itemINFO)
    {
        Debug.Log(itemID + ", " + itemLV + ", " + itemINFO);
        PlayerHaveItemData getItem = playerItemDB.Find(item => item.itemId == itemID);//현재 보유중인 ID인지 검색
        
        //미보유 아이템일경 리스트에 아이템 정보 추가
        if (getItem == null)
        {
            PlayerHaveItemData addItem = new PlayerHaveItemData();
            addItem.itemId = itemID;
            addItem.itemLevel = itemLV;
            addItem.itemInfo = itemINFO;
            playerItemDB.Add(addItem);
        }
        //있을 시 아이템 정보 수정
        else
        {
            int findItemIndex = playerItemDB.FindIndex(item => item.itemId == getItem.itemId);
            
            playerItemDB[findItemIndex].itemId = itemID;
            playerItemDB[findItemIndex].itemLevel = itemLV;
            playerItemDB[findItemIndex].itemInfo = itemINFO;
        }
    }

    //칩 관련 함수
    //칩 리스트를 가져오는 함수 outType 0:모든 칩 , 1:보유 중인 칩
    public List<CheepInfo> GetCheepList(int outType)
    {
        List<CheepInfo> returnList = new List<CheepInfo>();//리턴할 칩정보 리스트

        switch (outType) {
            //cheepDataBase의 모든 정보를 returnList에 삽입
            case 0:
                returnList = cheepDataBase;
                break;
            //cheepDataBased의 보유 중인 칩만 returnList에 삽입
            case 1:

                break;
        }

        return returnList;//칩 리스트 출력
    }
    
}