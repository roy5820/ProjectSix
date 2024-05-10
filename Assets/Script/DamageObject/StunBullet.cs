using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBullet : BulletBase
{
    public int sutunTurn = 0;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        //대상 스턴 구현
        CharacterController characterController = other.GetComponent<CharacterController>();
        if (characterController)
            characterController.GetComponent<CharacterController>().TransitionState(StateEnum.Sturn, sutunTurn);

    }
}
