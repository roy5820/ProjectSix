using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDController : MonoBehaviour
{
    EnemyController _eController;//�� ��Ʈ�ѷ�
    public Image hpBar;//ü�¹� �̹��� ������Ʈ
    private float nowHp;//���� ü��
    public Image actionIcon;//�ൿ �������� ǥ���� �̹��� ������Ʈ
    private Coroutine runningCoroutine = null;

    //���� Ÿ�԰� ������ ������ ���� Ŭ����
    [System.Serializable]
    public class ActionInfo {
        public StateEnum stateEnum;
        public Sprite icon;
    }

    //�׼� ���� Ÿ���� �Է� ���� ����Ʈ(���� �غ�� stateEnum ���� ���� �ൿ �������� ���� ���� ���)
    public List<ActionInfo> actionList;

    private void Start()
    {
        _eController = GetComponent<EnemyController>();//�� ��Ʈ�ѷ� ��������
    }

    private void Update()
    {
        
        //���� ���� ���� HUD ������Ʈ
        if (_eController)
        {
            if (_eController.direction == CharacterDirection.Right)
                this.transform.GetChild(0).localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            else
                this.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //ü�� ���� �� ü���� ���������� ��ȭ ��Ű�� �ڷ�ƾ ����
            float targetHp = _eController.NowHp;//��ǥ ü��
            if (nowHp != targetHp && runningCoroutine == null)
                runningCoroutine = StartCoroutine(IncreaseHpGauge(targetHp, 5f));
        }
    }

    //Hud������Ʈ �ϴ� �Լ�
    //�÷��̾� ü�¹ٸ� ��ǥ ü�� ���� ���������� ��ȭ��Ű�� �ڷ�ƾ
    IEnumerator IncreaseHpGauge(float targetHp, float fillSpeed)
    {
        float maxHp = _eController._characterStatus.maxHp;//�ִ� ü�� ��������
        float startHp = nowHp;
        while (true)
        {
            //���� äũ
            if ((startHp > targetHp && nowHp <= targetHp) || (startHp < targetHp && nowHp >= targetHp))
                break;
            nowHp += nowHp > targetHp ? -fillSpeed : fillSpeed;//���� ü�� ����
            hpBar.fillAmount = nowHp / maxHp;//�ִ�ü�¹� ����
            yield return new WaitForFixedUpdate();//����  ������ ��
        }
        //ü�¹� ������
        nowHp = targetHp;
        hpBar.fillAmount = nowHp / maxHp;

        runningCoroutine = null;
    }

    //���� �غ� �� �׼� ������ ���� �Լ�
    public void OnActionIcon(StateEnum state)
    {
        ActionInfo actionInfo = actionList.Find(actionList => actionList.stateEnum.Equals(state));
        if (actionInfo != null)
        {
            //������ �̹����������� ����
            if (actionInfo.icon != null)
            {
                actionIcon.sprite = actionInfo.icon;
                actionIcon.gameObject.SetActive(true);
            }
        }
    }

    //���� ���� �� �׼� ������ ���� �Լ�
    public void OffActionIcon()
    {
        actionIcon.gameObject.SetActive(false);//������ ��Ȱ��ȭ
    }
}