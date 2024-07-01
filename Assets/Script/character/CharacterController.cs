using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class CharacterController : MonoBehaviour
{
    protected GameManager gameManager = null;//게임 매니저를 가져와 저장할 변수
    protected BattleManager _battleManager = null;//배틀 매니저 가져올 변수

    [Header("Player Status")]
    public CharacterStatus _characterStatusOriginal;//캐릭터 스테이터스 오리지널 상태값
    public CharacterStatus _characterStatus;//캐릭터 스테이터스
    //현재체력 프로퍼티
    public int NowHp 
    {
        get
        {
            return _characterStatus.nowHp;
        }
        set
        {
            if (!isInvincibility)
            {
                _characterStatus.nowHp = value;
                //최대체력보다 높아지거나 0보다 작아지면 조정
                if (_characterStatus.nowHp > _characterStatus.maxHp) _characterStatus.nowHp = _characterStatus.maxHp;
                else if (_characterStatus.nowHp < 0) _characterStatus.nowHp = 0;
            }
        }
    }
    //캐릭터가 바라보는 방향
    public CharacterDirection direction = CharacterDirection.Right;
    public CharacterDirection Direction {
        get
        {
            return direction;
        }
        set
        {
            if (value == CharacterDirection.Right)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
            direction = value;
        }
    }
    public bool isTurnReady = false;//턴 준비 여부
    public bool isAvailabilityOfAction = true;//행동 가능 여부
    public bool isStatusProcessing = false;//상태 처리 여부
    public bool isInvincibility = false;//무적 여부
    public int delayTurn = 0;//행동 준비 턴 수

    public GameObject DeathEffect = null;
    //상태 정보
    [System.Serializable]
    public class StateInfo
    {
        public StateBase state;//상태
        public StateEnum stateEnum;//상태 열거형
    }
    [SerializeField]
    public List<StateInfo> _stateList;//캐릭터의 각 상태들을 답을 리스트
    protected CharacterStateContext characterStateContext;//캐릭터 상태 콘텍스트
    
    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;//게임 매니저 값 초기화
        _battleManager = BattleManager.Instance;//배틀 매니저 값 초기화
        characterStateContext = new CharacterStateContext(this);//상태콘택스트 초기화
        
        StatusValueSetting();
    }

    //캐릭터 턴 시작 처리
    public virtual void TurnStart()
    {
        isTurnReady = true;
    }

    //캐릭터 턴 종료 처리
    public virtual void TurnEnd()
    {
        isTurnReady = false;//턴 오버 처리
    }
    
    //상태명으로 상태 호출하는 함수
    public void TransitionState(StateEnum stateEnum, params object[] datas)
    {
        Debug.Log(stateEnum);
        CharacterState state = _stateList.Find(state => state.stateEnum.Equals(stateEnum)).state;//상태 타입으로 상태 가져오기
        characterStateContext.Transition(state, datas);
    }

    //상태 타입에 따른 연결된 상태구현 컴포넌트 바꾸는 함수
    public void ChangingStateByType(StateEnum stateEnum, StateBase changState)
    {
        int stateIndex = _stateList.FindIndex(state => state.stateEnum.Equals(stateEnum));//바꿀 상태의 리스트 위치 가져오기

        //바꾸려는 상태가 있으면 changState로 상태 변경
        if (stateIndex >= 0)
            _stateList[stateIndex].state = changState;
    }

    //캐릭터 상태값 초기화 함수
    public virtual void StatusValueSetting()
    {
        _characterStatus = ScriptableObject.CreateInstance<CharacterStatus>();
        _characterStatus.maxHp = _characterStatusOriginal.maxHp;
        _characterStatus.nowHp = _characterStatusOriginal.nowHp;
        _characterStatus.offensePower = _characterStatusOriginal.offensePower;
        _characterStatus.maxBattery = _characterStatusOriginal.maxBattery;
    }

    public virtual void IsDeath()
    {
        if (DeathEffect)
        {
            DeathEffect.SetActive(true);
        }
    }
}
