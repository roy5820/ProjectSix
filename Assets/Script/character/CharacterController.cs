using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public CharacterDirection direction { get; set; }//캐릭터가 바라보는 방향
    public bool isTurnReady = false;//턴 준비 여부
    public bool isAvailabilityOfAction = true;//행동 가능 여부
    public bool isStatusProcessing = false;//상태 처리 여부
    public bool isCharging = false;//공격 준비 여부
    public bool isInvincibility = false;//무적 여부
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

        //캐릭터 상태값 초기화
        _characterStatus = ScriptableObject.CreateInstance<CharacterStatus>();
        _characterStatus.maxHp = _characterStatusOriginal.maxHp;
        _characterStatus.nowHp = _characterStatusOriginal.nowHp;
        _characterStatus.offensePower = _characterStatusOriginal.offensePower;
        _characterStatus.maxBattery = _characterStatusOriginal.maxBattery;
        
    }

    protected virtual void Start()
    {
        direction = this.transform.localScale.x > 0 ? CharacterDirection.Right : CharacterDirection.Left;//캐릭터 방향 값 초기화
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
        CharacterState state = _stateList.Find(state => state.stateEnum.Equals(stateEnum)).state;//상태 명으로 상태 가져오기
        characterStateContext.Transition((CharacterState)state, datas);
    }
}
