using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    private CharacterController _characterController;
    public StateEnum useState;//아이템 사용시 사용할 상태

    private void Start()
    {
        _characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    //아이템 사용 처리
    public void ClickItem()
    {
        if(!_characterController.isStatusProcessing)
            _characterController.TransitionState(useState);
    }
}
