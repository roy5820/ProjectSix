using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    private GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수

    [Header("Player Status")]
    public int maxHp = 100;//최대체력
    private int nowHp;//현재 체력
    //현재체력 프로퍼티
    public int NowHp 
    {
        get
        {
            return nowHp;
        }
        set
        {
            nowHp += value;
            //최대체력보다 높아지거나 0보다 작아지면 조정
            if (nowHp > maxHp) nowHp = maxHp;
            else if(nowHp < 0) nowHp = 0;

        }
    }
    public int offensePower = 50;//공격력

    public CharacterDirection direction { get; set; }//캐릭터가 바라보는 방향
    public bool isTurnReady = false;//턴 준비 여부

    //캐릭터 각 상태들을 답을 변수(appearsState: 등장, forwardState: 전진, turnState: 방향 전환, hitState: 피격, dieState: 죽음 처리, attackState: 공격 처리
    private CharacterState appearsState, moveState, turnaboutState, hitState, dieState, normalAttackState;
    protected CharacterStateContext characterStateContext;//캐릭터 상태 콘텍스트

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화

        characterStateContext = new CharacterStateContext(this);//상태콘택스트 초기화
        nowHp = maxHp;//현재 체력 초기화
        direction = this.transform.localScale.x > 0 ? CharacterDirection.Left : CharacterDirection.Right;//캐릭터 방향 값 초기화

        //각 상태들의 기능을 구현한 컴포넌트를 추가하는 부분
        appearsState = gameObject.AddComponent<AppearsState>();
        moveState = gameObject.AddComponent<MoveState>();
        turnaboutState = gameObject.AddComponent<TurnaboutState>();
        hitState = gameObject.AddComponent<HitState>();
        dieState = gameObject.AddComponent<DieState>();
        normalAttackState = gameObject.AddComponent<NormalAttackState>();
    }
    //캐릭터 턴 시작 처리
    protected virtual void TurnStart()
    {
        isTurnReady = true;
    }

    //캐릭터 턴 종료 처리
    public virtual void TurnEnd()
    {
        isTurnReady = false;//턴 오버 처리
    }
    

    //각 상태별로 호출하는 함수
    //등장 상태 호출 함수
    public void AppearsState()
    {
        characterStateContext.Transition(appearsState);
    }

    //전진 상태 호출 함수
    public void MoveState(CharacterDirection direction)
    {
        this.GetComponent<MoveState>().moveDirection = direction;//이동 방향값 설정
        characterStateContext.Transition(moveState);
    }

    //방향 전환 상태 호출 함수
    public void TurnaboutState()
    {
        characterStateContext.Transition(turnaboutState );
    }

    //피격 상태 호출 함수
    public void HitState(int damage)
    {
        Debug.Log("isDamage" + damage);
        this.GetComponent<HitState>().hitDamage = damage;//데미지값 설정
        Debug.Log("HitDamage" + this.GetComponent<HitState>().hitDamage);
        characterStateContext.Transition(hitState);
    }

    //죽음 상태 호출 함수
    public void DieState()
    {
        characterStateContext.Transition(dieState);
    }


    //공격 상태 호출 함수
    public void NormalAttackState()
    {
        characterStateContext.Transition(normalAttackState);
    }

    //상태 호출 함수 stateName: 호출할 상태 함수 명
    public void OnNameToState(string stateName)
    {
        // stateName과 일치하는 함수를 찾아 실행
        System.Reflection.MethodInfo method = GetType().GetMethod(stateName);
        if (method != null)
        {
            method.Invoke(this, null);
        }
    }
}
