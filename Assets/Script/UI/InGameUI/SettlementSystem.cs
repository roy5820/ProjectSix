using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour
{
    public GameObject settlementWIndow;//결산 표시 창
    public Text resultTxt;//성과표시 텍스트
    //결산 표시 텍스트
    public Text stageRewards;
    public Text stageBonus;
    public Text moneyHeld;
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

    //스테이지 클리어 이벤트 발생 시 결산 구현
    public void StageClear()
    {
        settlementWIndow.SetActive(true);

        //게임, 배틀 메니저 가져오기
        GameManager _gameManager = GameManager.Instance;
        BattleManager _battleManager = BattleManager.Instance;

        //스테이지 성과에 따른 결과 표시 기능 구현
        string battleAchievements = null;
        int bonusAmount = 0;
        int clearTurn = _battleManager.nowTurnCnt;
        int bestTurn = _battleManager.nowStage.bestTurn;
        int worstTurn = _battleManager.nowStage.worstTurn;

        if (clearTurn < bestTurn)
        {
            battleAchievements = "BEST";
            bonusAmount = (int)(_battleManager.stageRewards * 0.3f);
        }
        else if(clearTurn > worstTurn)
        {
            battleAchievements = "WORST";
            bonusAmount = -(int)(_battleManager.stageRewards * 0.3f);
        }
        else
        {
            battleAchievements = "NORMAL";
        }
        _gameManager.moneyHeld += (_battleManager.stageRewards + bonusAmount);

        resultTxt.text = battleAchievements;
        stageRewards.text = _battleManager.stageRewards.ToString();
        stageBonus.text = bonusAmount.ToString();
        moneyHeld.text = _gameManager.moneyHeld.ToString();
    }
}
