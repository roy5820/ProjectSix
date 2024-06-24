using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfoNotation : MonoBehaviour
{
    private BattleManager _battleManager;//���� �޴���
    public float preInterval = 30;//��ư ����
    public GameObject waveNotationPre;
    private List<GameObject> preList = new List<GameObject>();

    //Ȱ��ȭ�� �̺�Ʈ ����
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.TurnStart, TurnStart);//TurnStart �̺�Ʈ ����
        TurnEventBus.Subscribe(TurnEventType.StageClear, ClearWaveInfo);
        TurnEventBus.Subscribe(TurnEventType.Win, ClearWaveInfo);
    }

    //��Ȱ��ȭ�� �̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.TurnStart, TurnStart);//TurnStart �̺�Ʈ ����
        TurnEventBus.Unsubscribe(TurnEventType.StageClear, ClearWaveInfo);
        TurnEventBus.Unsubscribe(TurnEventType.Win, ClearWaveInfo);
    }

    // Start is called before the first frame update
    void Start()
    {
        _battleManager = BattleManager.Instance;//���� �޴��� ��������
    }

    private void TurnStart()
    {
        if(_battleManager == null)
            _battleManager = BattleManager.Instance;//���� �޴��� ��������

        int waveCnt = _battleManager.nowStage.waveList.Count;
        
        if (preList.Count == 0)
        {
            
            // ��ư�� �� ������ �̿��� ó�� ��ư�� ��ġ ���
            float startX = -(waveCnt - 1) * preInterval / 2f;

            //�÷��̾� ������ DB�� �������� ������ ��� ��ư ���� �� ��ġ
            for (int i = 0; i < waveCnt; i++)
            {
                //������ ���� ���� �� ������ ����
                GameObject notationPre = Instantiate(waveNotationPre, this.transform);
                Debug.Log(notationPre);
                Debug.Log(preList);
                preList.Add(notationPre);

                // ��ư�� ��ġ ���
                float posX = startX + i * preInterval;

                // ��ư�� RectTransform �����ͼ� ��ġ ����
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
