using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHUDController : MonoBehaviour
{
    private CharacterController _characterController;//캐릭터 컨트롤러
    public GameObject hpBarPanel;//hp바 표현 패널
    public Image hpBar;//보스 체력바 
    private float nowHp = 0;//현재 체력
    public float fillSpeed = 10;//체력바 변경 시 체우는 속도
    public float minHpFillAmount = 0.05f;//최소 HP바 크기
    private Coroutine runningCoroutine = null;//현재 실행중인 코루틴

    // Start is called before the first frame update
    void Start()
    {
        GameObject bossObj = GameObject.FindGameObjectWithTag("Boss");
        if (bossObj != null)
            _characterController = bossObj.GetComponent<CharacterController>();//캐릭터 매니저 초기화
        else
            hpBarPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((_characterController != null))
        {
            float targetHp = _characterController.NowHp;//목표 체력
            //체력 변동 시 체력을 점진적으로 변화 시키는 코루틴 실행
            if (nowHp != targetHp && runningCoroutine == null)
            {
                runningCoroutine = StartCoroutine(IncreaseHpGauge(targetHp, fillSpeed));
            }
        }
    }

    //플레이어 체력바를 목표 체력 값을 점진적으로 변화시키는 코루틴
    IEnumerator IncreaseHpGauge(float targetHp, float fillSpeed)
    {
        float fillAmount = 0;
        float maxHp = _characterController._characterStatus.maxHp;//최대 체력 가져오기
        float startHp = nowHp;
        
        while (true)
        {
            //조건 채크
            if ((startHp > targetHp && nowHp <= targetHp) || (startHp < targetHp && nowHp >= targetHp))
                break;
            nowHp += nowHp > targetHp ? -fillSpeed : fillSpeed;//현재 체력 갱신
            fillAmount = nowHp / maxHp;//최대체력바 비율 계산

            hpBar.fillAmount = fillAmount >= minHpFillAmount || nowHp == 0 ? fillAmount : minHpFillAmount;//최대체력바 갱신
            yield return new WaitForFixedUpdate();//증가  딜레이 폭
        }
        //체력바 재조정
        nowHp = targetHp;
        fillAmount = nowHp / maxHp;//최대체력바 비율 계산
        hpBar.fillAmount = fillAmount >= minHpFillAmount || nowHp == 0 ? fillAmount : minHpFillAmount;//최대체력바 갱신
        runningCoroutine = null;
    }
}
