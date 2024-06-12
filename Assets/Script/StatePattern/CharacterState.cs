
public interface CharacterState
{
    CharacterStateContext CharacterStateContext { get; set; }
    CharacterStateContext CharacterStateContext1 { get; set; }

    void Handle(CharacterController controller, params object[] datas);
}