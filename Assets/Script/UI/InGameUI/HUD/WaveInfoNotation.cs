using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfoNotation : MonoBehaviour
{
    private BattleManager _battleManager;//게임 메니저
    public float preInterval = 30;//버튼 간격
    public GameObject waveNotationPre;
    private List<GameObject> preList = new List<GameObject>();

    //활성화시 이벤트 설정
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 설정
        TurnEventBus.Subscribe(TurnEventType.StageClear, ClearWaveInfo);
        TurnEventBus.Subscribe(TurnEventType.Win, ClearWaveInfo);
    }

    //비활성화시 이벤트 제거
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart 이벤트 제거
        TurnEventBus.Unsubscribe(TurnEventType.StageClear, ClearWaveInfo);
        TurnEventBus.Unsubscribe(TurnEventType.Win, ClearWaveInfo);
    }

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = BattleManager.Instance;//게임 메니저 가져오기
    }

    private void TurnStart()
    {
        if(_battleManager == null)
            _battleManager = BattleManager.Instance;//게임 메니저 가져오기

        int waveCnt = _battleManager.nowStage.waveList.Count;
        
        if (preList.Count == 0)
        {
            
            // 버튼의 총 개수를 이용해 처음 버튼의 위치 계산
            float startX = -(waveCnt - 1) * preInterval / 2f;

            //플레이어 아이템 DB를 바탕으로 아이템 사용 버튼 생성 및 배치
            for (int i = 0; i < waveCnt; i++)
            {
                //아이템 정보 갱신 후 프리펩 생성
                GameObject notationPre = Instantiate(waveNotationPre, this.transform);
                Debug.Log(notationPre);
                Debug.Log(preList);
                preList.Add(notationPre);

                // 버튼의 위치 계산
                float posX = startX + i * preInterval;

                // 버튼의 RectTransform 가져와서 위치 설정
                RectTransform rectTransform = notationPre.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);
            }
        }
        else
        {
            Debug.Log(waveCnt - (_battleManager.nextWaveNum - 1));
            for (int i = waveCnt-(_battleManager.nextWaveNum-1); i < waveCnt; i++)
            {
                preList[i].gameObject.SetActive(false);
            }
        }
        
    }

    private void ClearWaveInfo()
    {
        int waveCnt = _battleManager.nowStage.waveList.Count;
        for (int i = 0; i < waveCnt; i++)
        {
            preList[i].gameObject.SetActive(false);
        }
    }
}
