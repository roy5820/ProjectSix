using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBase
{
    public float fireTime = 5f;//������ �߻� �ð�
    private float nowTime = 0;
    public GameObject LaserLight;//������ ����Ʈ

    protected override void FixedUpdate()
    {
        //�߻� �ð��� ������ ������ ����
        if(isFire && nowTime >= fireTime)
            Destroy(this.gameObject);

        if (isFire && fireDir != 0)
        {
            nowTime += Time.deltaTime;
        }
    }

    public override void OnFire()
    {
        
        LaserLight.SetActive(true);
        base.OnFire();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//������ �ο�
    }
}
