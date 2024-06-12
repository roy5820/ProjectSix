
public interface CharacterState
{
    CharacterStateContext CharacterStateContext { get; set; }

    void Handle(CharacterController controller, params object[] datas);
}