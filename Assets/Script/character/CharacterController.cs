using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수

    public int maxHp = 100;//최대체력
    public int nowHp { get; set; }//현재체력

    public int maxShild = 20;//최대 보호막 량
    public int nowShild { get; set; }//현재 보호막 량

    public CharacterDirection direction { get; set; }//캐릭터가 바라보는 방향

    //캐릭터 각 상태들을 답을 변수(appearsState: 등장, forwardState: 전진, turnState: 방향 전환, hitState: 피격, dieState: 죽음 처리
    private CharacterState appearsState, forwardState, turnaboutState, hitState, dieState;
    private CharacterStateContext characterStateContext;//캐릭터 상태 콘텍스트

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화

        characterStateContext = new CharacterStateContext(this);//상태콘택스트 초기화
        nowHp = maxHp;//현재 체력 초기화
        nowShild = maxShild;//현재 보호막 초기화

        //각 상태들의 기능을 구현한 컴포넌트를 추가하는 부분
        appearsState = gameObject.GetComponent<CharacterAppearsState>();
        forwardState = gameObject.GetComponent<CharacterForwardState>();
        turnaboutState = gameObject.GetComponent<CharacterTurnaboutState>();
        hitState = gameObject.GetComponent<CharacterHitState>();
        dieState = gameObject.GetComponent<CharacterDieState>();
    }

    protected virtual void Update()
    {
        
    }

    //각 상태별로 호출하는 함수
    //등장 상태 호출 함수
    public void AppearsState()
    {
        characterStateContext.Transition(appearsState);
    }

    //전진 상태 호출 함수
    public void ForwardState()
    {
        characterStateContext.Transition(forwardState);
    }

    //방향 전환 상태 호출 함수
    public void TurnaboutState()
    {
        characterStateContext.Transition(turnaboutState);
    }

    //피격 상태 호출 함수
    public void HitState()
    {
        characterStateContext.Transition(hitState);
    }

    //죽음 상태 호출 함수
    public void DieState()
    {
        characterStateContext.Transition(dieState);
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
