using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheepBase : MonoBehaviour
{
    protected CharacterController _characterController = null;//캐릭터 컨트롤러

    private void Start()
    {
        _characterController = transform.parent.parent.GetComponent<CharacterController>();//캐릭터 컨트롤러
    }

    //칩기능 구현을 할 부분
    public abstract void ActivateChipEffect();
}