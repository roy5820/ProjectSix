using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class CharacterController : MonoBehaviour
{
    protected GameManager gameManager = null;//���� �Ŵ����� ������ ������ ����
    protected BattleManager _battleManager = null;//��Ʋ �Ŵ��� ������ ����

    [Header("Player Status")]
    public CharacterStatus _characterStatusOriginal;//ĳ���� �������ͽ� �������� ���°�
    public CharacterStatus _characterStatus;//ĳ���� �������ͽ�
    //����ü�� ������Ƽ
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
                //�ִ�ü�º��� �������ų� 0���� �۾����� ����
                if (_characterStatus.nowHp > _characterStatus.maxHp) _characterStatus.nowHp = _characterStatus.maxHp;
                else if (_characterStatus.nowHp < 0) _characterStatus.nowHp = 0;
            }
        }
    }
    //ĳ���Ͱ� �ٶ󺸴� ����
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
    public bool isTurnReady = false;//�� �غ� ����
    public bool isAvailabilityOfAction = true;//�ൿ ���� ����
    public bool isStatusProcessing = false;//���� ó�� ����
    public bool isInvincibility = false;//���� ����
    public int delayTurn = 0;//�ൿ �غ� �� ��

    public GameObject DeathEffect = null;
    //���� ����
    [System.Serializable]
    public class StateInfo
    {
        public StateBase state;//����
        public StateEnum stateEnum;//���� ������
    }
    [SerializeField]
    public List<StateInfo> _stateList;//ĳ������ �� ���µ��� ���� ����Ʈ
    protected CharacterStateContext characterStateContext;//ĳ���� ���� ���ؽ�Ʈ
    
    protected virtual void Awake()
    {
        gameManager = GameManager.Instance;//���� �Ŵ��� �� �ʱ�ȭ
        _battleManager = BattleManager.Instance;//��Ʋ �Ŵ��� �� �ʱ�ȭ
        characterStateContext = new CharacterStateContext(this);//�������ý�Ʈ �ʱ�ȭ
        
        StatusValueSetting();
    }

    //ĳ���� �� ���� ó��
    public virtual void TurnStart()
    {
        isTurnReady = true;
    }

    //ĳ���� �� ���� ó��
    public virtual void TurnEnd()
    {
        isTurnReady = false;//�� ���� ó��
    }
    
    //���¸����� ���� ȣ���ϴ� �Լ�
    public void TransitionState(StateEnum stateEnum, params object[] datas)
    {
        Debug.Log(stateEnum);
        CharacterState state = _stateList.Find(state => state.stateEnum.Equals(stateEnum)).state;//���� Ÿ������ ���� ��������
        characterStateContext.Transition(state, datas);
    }

    //���� Ÿ�Կ� ���� ����� ���±��� ������Ʈ �ٲٴ� �Լ�
    public void ChangingStateByType(StateEnum stateEnum, StateBase changState)
    {
        int stateIndex = _stateList.FindIndex(state => state.stateEnum.Equals(stateEnum));//�ٲ� ������ ����Ʈ ��ġ ��������

        //�ٲٷ��� ���°� ������ changState�� ���� ����
        if (stateIndex >= 0)
            _stateList[stateIndex].state = changState;
    }

    //ĳ���� ���°� �ʱ�ȭ �Լ�
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
