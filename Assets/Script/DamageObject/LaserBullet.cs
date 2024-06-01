using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBase
{
    public float fireTime = 5f;//������ �߻� �ð�
    private float nowTime = 0;

    protected override void FixedUpdate()
    {
        //�߻� �ð��� ������ ������ ����
        if(nowTime >= fireTime)
            Destroy(this.gameObject);

        if (isFire && fireDir != 0)
        {
            nowTime += Time.deltaTime;
        }
    }

    public override void OnFire()
    {
        //�Ѿ� ���⿡ ���� ��ġ ����
        if (fireDir == CharacterDirection.Left)
            transform.localScale = new Vector3(-1, 1, 1);
        base.OnFire();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//������ �ο�
    }
}
