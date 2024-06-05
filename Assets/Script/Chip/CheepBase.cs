using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheepBase : MonoBehaviour
{
    protected CharacterController _characterController = null;//캐릭터 컨트롤러


    //칩기능 구현을 할 부분
    public virtual void ActivateChipEffect()
    {
        _characterController = transform.GetComponentInParent<CharacterController>();//캐릭터 컨트롤러
    }
}