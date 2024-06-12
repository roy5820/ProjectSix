using System.Collections;
using UnityEngine;

public abstract class StateBase : MonoBehaviour, CharacterState
{
    protected CharacterController characterController = null;//ĳ���� ��Ʈ�ѷ�
    protected BattleManager _battleManager = null;//��Ʋ �Ŵ���
    protected Animator _animator = null;//ĳ���� �ִϸ��̼�

    public string stateAniParamater = "";
    public float sateDelayTime = 0.5f;//���� ������ �ð�
    public SoundManger _soundManger = null;//���� �޴���
    public AudioClip stateSound = null;//���� �� ����� ����

    public CharacterState CharacterState
    {
        get => default;
        set
        {
        }
    }

    public void Handle(CharacterController characterController, params object[] datas)
    {
        //ĳ���� ��Ʈ�ѷ� �� �ʱ�ȭ
        if (!this.characterController)
            this.characterController = characterController;
        //��Ʋ �Ŵ��� �� �ʱ�ȭ
        if(!this._battleManager)
            this._battleManager = BattleManager.Instance;
        //ĳ���� �ִϸ����� ��������
        if (!this._animator)
            characterController.gameObject.TryGetComponent<Animator>(out _animator);
        //ĳ���� �ൿ �ִϸ��̼� ���
        if (!string.IsNullOrEmpty(stateAniParamater) && this._animator && _animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
            _animator.SetTrigger(stateAniParamater);
        //���� �޴��� ��������
        if(!_soundManger)
            _soundManger = SoundManger.Instance;
        //���� ���
        if (_soundManger && stateSound)
            _soundManger.PlaySFX(stateSound);

        characterController.isStatusProcessing = true;//ĳ���� ���� ó�� ��

        StartCoroutine(StateFuntion(datas));//��� ���� �ڷ�ƾ �Լ� ȣ��
    }

    //��� �޾� ����� ������ �κ�
    protected virtual IEnumerator StateFuntion(params object[] datas)
    {
        yield return new WaitForSeconds(0.2f);
        characterController.isStatusProcessing = false;//ĳ���� ���� ó�� ����
        yield return null;
    }
}
