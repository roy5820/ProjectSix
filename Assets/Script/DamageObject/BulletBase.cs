using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float speed = 60f;//발사 속도
    public int damage;//데미지
    protected bool isFire = true;//발사 여부
    public CharacterDirection fireDir = 0;//발사방향
    private Rigidbody2D rBody;//리지드 바디
    private Animator thisAnimation;
    public float destroyDelay = 0.5f;
    public AudioClip fireSound = null;//발사 사운드
    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();//리지드바디 초기화
        thisAnimation = GetComponent<Animator>();
    }

    public virtual void OnFire()
    {
        if(fireSound)
            SoundManger.Instance.PlaySFX(fireSound);
        GetComponent<Collider2D>().enabled = true;
        if (thisAnimation != null)
            thisAnimation.SetTrigger("FIre");
        //총알 방향에 따른 위치 조정
        if (fireDir == CharacterDirection.Left)
            transform.localScale = new Vector3(-1, 1, 1);
        isFire = true;
    }

    protected virtual void FixedUpdate()
    {
        //발사 구현
        if (isFire && fireDir != 0)
        {
            Vector3 dir = new Vector3((int)fireDir, 0, 0) * speed;//발사 백터 계산
            rBody.velocity = dir;//총알 발사 구현
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(thisAnimation != null)
            thisAnimation.SetTrigger("Destroy");
            
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//데미지 부여
        rBody.velocity = Vector3.zero;
        isFire = false;
        Invoke("ThisDestroy", destroyDelay);
    }

    private void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
