using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDController : MonoBehaviour
{
    private CharacterController _characterController;//배틀매니저
    public Image hpBar;//플레이어 체력바 
    private float nowHp = 0;//현재 체력
    private Coroutine runningCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();//캐릭터 매니저 초기화
        //nowHp = _characterController.NowHp;//현재 체력 정보 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if ((_characterController))
        {
            float targetHp = _characterController.NowHp;//목표 체력
            //체력 변동 시 체력을 점진적으로 변화 시키는 코루틴 실행
            if (nowHp != targetHp && runningCoroutine ==  null)
                runningCoroutine = StartCoroutine(IncreaseHpGauge(targetHp, 10f));
        }
        
    }

    //플레이어 체력바를 목표 체력 값을 점진적으로 변화시키는 코루틴
    IEnumerator IncreaseHpGauge(float targetHp,  float fillSpeed)
    {
        float maxHp = _characterController._characterStatus.maxHp;//최대 체력 가져오기
        float startHp = nowHp;
        while (true)
        {
            //조건 채크
            if ((startHp > targetHp && nowHp <= targetHp) || (startHp < targetHp && nowHp >= targetHp))
                break;
            nowHp += nowHp > targetHp ? -fillSpeed : fillSpeed;//현재 체력 갱신
            hpBar.fillAmount  = nowHp  / maxHp;//최대체력바 갱신
            yield return new WaitForFixedUpdate();//증가  딜레이 폭
        }
        //체력바 재조정
        nowHp = targetHp;
        hpBar.fillAmount = nowHp / maxHp;

        runningCoroutine = null;
    }
}
