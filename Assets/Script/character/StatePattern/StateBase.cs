using UnityEngine;

public class StateBase : MonoBehaviour, CharacterState
{
    private CharacterController characterController;

    public void Handle(CharacterController characterController)
    {
        if (!this.characterController)
            this.characterController = characterController;

        //자식에서 override하여 작성할부분
        //호출 시 기능을 구현할 부분
        
    }
}
