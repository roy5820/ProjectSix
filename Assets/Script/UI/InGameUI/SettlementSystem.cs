using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour
{
    public GameObject settlementWIndow;//��� ǥ�� â
    public Text resultTxt;//����ǥ�� �ؽ�Ʈ
    //��� ǥ�� �ؽ�Ʈ
    public Text stageRewards;
    public Text stageBonus;
    public Text beforMoneyHeld;
    public Text afterMoneyHeld;
    //Ȱ��ȭ�� �̺�Ʈ ����
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.StageClear, StageClear);//TurnStart �̺�Ʈ ����
    }

    //��Ȱ��ȭ�� �̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.StageClear, StageClear);//TurnStart �̺�Ʈ ����
    }

    //�������� Ŭ���� �̺�Ʈ �߻� �� ��� ����
    public void StageClear()
    {
        settlementWIndow.SetActive(true);

        //����, ��Ʋ �޴��� ��������
        GameManager _gameManager = GameManager.Instance;
        BattleManager _battleManager = BattleManager.Instance;

        //�������� ������ ���� ��� ǥ�� ��� ����
        string battleAchievements = null;
        int afterMoney = _gameManager.moneyHeld;//���� �ܾ�
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

        beforMoneyHeld.text = afterMoney.ToString();
        resultTxt.text = battleAchievements;
        stageRewards.text = _battleManager.stageRewards.ToString();
        stageBonus.text = bonusAmount.ToString();
        afterMoneyHeld.text = _gameManager.moneyHeld.ToString();
    }
}
