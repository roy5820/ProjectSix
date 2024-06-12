using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageHUD : MonoBehaviour
{
    private BattleManager _battleManager;//��Ʋ �޴���

    public Text nowTurnInfoHUD;//���� �� ǥ���ϴ� text������Ʈ
    public Text stageRewardsHUD;//���� �������� ������ ǥ���ϴ� Text������Ʈ
    private int stageRewards = 0;//�������� ����
    private Coroutine runningCoroutine = null;//���� ���� ���� �ڷ�ƾ
    public int increaseSpeed = 3;//�������� ���� ���� �ӵ�
    public AudioClip acquisitionSound;//�� ȹ�� ����

    private void Start()
    {
        _battleManager = BattleManager.Instance;//��Ʋ �Ŵ��� �ʱ�ȭ
    }

    private void Update()
    {
        //HUD ������Ʈ
        if (_battleManager)
        {
            nowTurnInfoHUD.text = (_battleManager.nowTurnCnt).ToString();//�� ���� ����

            //�������� ���� ���� ó�� �κ�
            int targetValue = _battleManager.stageRewards;
            if (stageRewards < targetValue && runningCoroutine == null)
                runningCoroutine = StartCoroutine(IncreaseStageRewards(targetValue, increaseSpeed));

            stageRewardsHUD.text = stageRewards.ToString();
        }
    }

    //�������� ������ ���������� ������Ű�� �ڷ�ƾ �Լ�
    IEnumerator IncreaseStageRewards(int targetValue, int increaseSpeed)
    {
        SoundManger.Instance.PlaySFX(acquisitionSound);
        while(stageRewards < targetValue)
        {
            stageRewards+= increaseSpeed;
            if (stageRewards > targetValue)
                stageRewards = targetValue;
            yield return new WaitForFixedUpdate();
        }

        

        runningCoroutine = null;
    }
}
