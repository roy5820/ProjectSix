using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUDController : MonoBehaviour
{
    EnemyController _eController;//적 컨트롤러
    public Image hpBar;//체력바 이미지 오브젝트
    private float nowHp;//현제 체력
    public Image actionIcon;//행동 아이콘을 표기할 이미지 오브젝트
    private Coroutine runningCoroutine = null;
    public float fillSpeed = 10;//hp바 변경 속도
    //상태 타입과 아이콘 정보를 가진 클래스
    [System.Serializable]
    public class ActionInfo {
        public StateEnum stateEnum;
        public Sprite icon;
    }

    //액션 정보 타입을 입력 받을 리스트(공격 준비시 stateEnum 값에 따라 행동 아이콘을 띄우기 위해 사용)
    public List<ActionInfo> actionList;

    private void Start()
    {
        _eController = GetComponent<EnemyController>();//적 컨트롤러 가져오기
    }

    private void Update()
    {
        
        //상태 값에 따른 HUD 없데이트
        if (_eController)
        {
            if (_eController.direction == CharacterDirection.Right)
                this.transform.GetChild(0).localScale = new Vector3(-0.1f, 0.1f, 0.1f);
            else
                this.transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //체력 변동 시 체력을 점진적으로 변화 시키는 코루틴 실행
            float targetHp = _eController.NowHp;//목표 체력
            if (nowHp != targetHp && runningCoroutine == null)
                runningCoroutine = StartCoroutine(IncreaseHpGauge(targetHp, fillSpeed));
        }
    }

    //Hud업데이트 하는 함수
    //플레이어 체력바를 목표 체력 값을 점진적으로 변화시키는 코루틴
    IEnumerator IncreaseHpGauge(float targetHp, float fillSpeed)
    {
        float maxHp = _eController._characterStatus.maxHp;//최대 체력 가져오기
        float startHp = nowHp;
        while (true)
        {
            //조건 채크
            if ((startHp > targetHp && nowHp <= targetHp) || (startHp < targetHp && nowHp >= targetHp))
                break;
            nowHp += nowHp > targetHp ? -fillSpeed : fillSpeed;//현재 체력 갱신
            hpBar.fillAmount = nowHp / maxHp;//최대체력바 갱신
            yield return new WaitForFixedUpdate();//증가  딜레이 폭
        }
        //체력바 재조정
        nowHp = targetHp;
        hpBar.fillAmount = nowHp / maxHp;

        runningCoroutine = null;
    }

    //공격 준비 시 액션 아이콘 띄우는 함수
    public void OnActionIcon(StateEnum state)
    {
        ActionInfo actionInfo = actionList.Find(actionList => actionList.stateEnum.Equals(state));
        if (actionInfo != null)
        {
            //아이콘 이미지가있으면 띄우기
            if (actionInfo.icon != null)
            {
                actionIcon.sprite = actionInfo.icon;
                actionIcon.gameObject.SetActive(true);
            }
        }
    }

    //공격 실행 시 액션 아이콘 끄는 함수
    public void OffActionIcon()
    {
        actionIcon.gameObject.SetActive(false);//아이콘 비활성화
    }
}
