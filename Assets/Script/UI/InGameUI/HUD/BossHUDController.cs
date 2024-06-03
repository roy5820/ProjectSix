using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUDController : MonoBehaviour
{
    private CharacterController _characterController;//ĳ���� ��Ʈ�ѷ�
    public GameObject hpBarPanel;//hp�� ǥ�� �г�
    public Image hpBar;//���� ü�¹� 
    private float nowHp = 0;//���� ü��
    public float fillSpeed = 10;//ü�¹� ���� �� ü��� �ӵ�
    public float minHpFillAmount = 0.05f;//�ּ� HP�� ũ��
    private Coroutine runningCoroutine = null;//���� �������� �ڷ�ƾ

    // Start is called before the first frame update
    void Start()
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
            _characterController = bossObj.GetComponent<CharacterController>();//ĳ���� �Ŵ��� �ʱ�ȭ
        else
            hpBarPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((_characterController != null))
        {
            float targetHp = _characterController.NowHp;//��ǥ ü��
            //ü�� ���� �� ü���� ���������� ��ȭ ��Ű�� �ڷ�ƾ ����
            if (nowHp != targetHp && runningCoroutine == null)
            {
                runningCoroutine = StartCoroutine(IncreaseHpGauge(targetHp, fillSpeed));
            }
        }
    }

    //�÷��̾� ü�¹ٸ� ��ǥ ü�� ���� ���������� ��ȭ��Ű�� �ڷ�ƾ
    IEnumerator IncreaseHpGauge(float targetHp, float fillSpeed)
    {
        float fillAmount = 0;
        float maxHp = _characterController._characterStatus.maxHp;//�ִ� ü�� ��������
        float startHp = nowHp;
        
        while (true)
        {
            //���� äũ
            if ((startHp > targetHp && nowHp <= targetHp) || (startHp < targetHp && nowHp >= targetHp))
                break;
            nowHp += nowHp > targetHp ? -fillSpeed : fillSpeed;//���� ü�� ����
            fillAmount = nowHp / maxHp;//�ִ�ü�¹� ���� ���

            hpBar.fillAmount = fillAmount >= minHpFillAmount || nowHp == 0 ? fillAmount : minHpFillAmount;//�ִ�ü�¹� ����
            yield return new WaitForFixedUpdate();//����  ������ ��
        }
        //ü�¹� ������
        nowHp = targetHp;
        fillAmount = nowHp / maxHp;//�ִ�ü�¹� ���� ���
        hpBar.fillAmount = fillAmount >= minHpFillAmount || nowHp == 0 ? fillAmount : minHpFillAmount;//�ִ�ü�¹� ����
        runningCoroutine = null;
    }
}
