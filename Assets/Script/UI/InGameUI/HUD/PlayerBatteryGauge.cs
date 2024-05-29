using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBatteryGauge : MonoBehaviour
{
    private PlayerController _playerController;//�÷��̾� ��Ʈ�ѷ�

    public List<GameObject> batteryImageList;//��ư ����Ʈ
    public float batteryInterval = 40;//���͸� ����
    public GameObject batteryPre;//���͸� ������
    public Sprite batteryOnImg;//���͸� ������ �� �̹���
    public Sprite batteryOffImg;//���͸� ������ �� �̹���

    private Coroutine runningCoroutine = null;//���� �������� �ڷ�ƾ

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();//ĳ���� �Ŵ��� �ʱ�ȭ

        StartCoroutine(SetMaxBattery());
    }

    private void Update()
    {
        //���͸� �� ����
        if (_playerController)
        {
            

            //���� ���͸��� �� ����
            for (int i = 0; i < batteryImageList.Count; i++)
            {
                Sprite batteryImg = i < _playerController.nowBattery ? batteryOnImg : batteryOffImg;//���� ���͸� ��ġ�� ���� �̹��� ����
                batteryImageList[i].GetComponent<Image>().sprite = batteryImg;  
            }
        }
    }

    public IEnumerator SetMaxBattery()
    {
        //�ִ� ���͸� �뷮 ��ŭ 
        // ��ư�� �� ������ �̿��� ó�� ��ư�� ��ġ ���
        float startX = -(_playerController._characterStatus.maxBattery - 1) * batteryInterval / 2f;
        //�÷��̾� ������ DB�� �������� ������ ��� ��ư ���� �� ��ġ
        for (int i = 0; i < _playerController._characterStatus.maxBattery; i++)
        {
            GameObject battery = Instantiate(batteryPre, transform);

            // ��ư�� ��ġ ���
            float posX = startX + i * batteryInterval;

            // battery�� RectTransform �����ͼ� ��ġ ����
            RectTransform rectTransform = battery.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(posX, rectTransform.anchoredPosition.y);

            //battery Image������Ʈ ����Ʈ�� �ֱ�
            batteryImageList.Add(battery);
        }
        runningCoroutine = null;
        yield return null;
    }
}
