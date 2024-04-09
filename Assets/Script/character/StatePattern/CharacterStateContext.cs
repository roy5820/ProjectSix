
public class CharacterStateContext
{
    public CharacterState CurrentState
    {
        get; set;
    }

    private readonly CharacterController characterController;


    public CharacterStateContext(CharacterController characterController)
    {
        this.characterController = characterController;
    }

    //현재 상태 업데이트
    public void Transition()
    {
        CurrentState.Handle(characterController);
    }

    //상태 전환
    public void Transition(CharacterState state, params object[] datas)
    {
        CurrentState = state;
        CurrentState.Handle(characterController, datas);
    }
}
