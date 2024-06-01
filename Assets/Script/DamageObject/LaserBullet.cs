using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBase
{
    public float fireTime = 5f;//레이저 발사 시간
    private float nowTime = 0;

    protected override void FixedUpdate()
    {
        //발사 시간이 지나면 레이저 삭제
        if(nowTime >= fireTime)
            Destroy(this.gameObject);

        if (isFire && fireDir != 0)
        {
            nowTime += Time.deltaTime;
        }
    }

    public override void OnFire()
    {
        //총알 방향에 따른 위치 조정
        if (fireDir == CharacterDirection.Left)
            transform.localScale = new Vector3(-1, 1, 1);
        base.OnFire();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//데미지 부여
    }
}
