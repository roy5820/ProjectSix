using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheepBase : MonoBehaviour
{
    protected CharacterController _characterController = null;//ĳ���� ��Ʈ�ѷ�


    //Ĩ��� ������ �� �κ�
    public virtual void ActivateChipEffect()
    {
        _characterController = transform.GetComponentInParent<CharacterController>();//ĳ���� ��Ʈ�ѷ�
    }
}