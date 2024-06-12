using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public float speed = 60f;//�߻� �ӵ�
    public int damage;//������
    protected bool isFire = true;//�߻� ����
    public CharacterDirection fireDir = 0;//�߻����
    private Rigidbody2D rBody;//������ �ٵ�
    private Animator thisAnimation;
    public float destroyDelay = 0.5f;
    public AudioClip fireSound = null;//�߻� ����
    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();//������ٵ� �ʱ�ȭ
        thisAnimation = GetComponent<Animator>();
    }

    public virtual void OnFire()
    {
        if(fireSound)
            SoundManger.Instance.PlaySFX(fireSound);
        GetComponent<Collider2D>().enabled = true;
        if (thisAnimation != null)
            thisAnimation.SetTrigger("FIre");
        //�Ѿ� ���⿡ ���� ��ġ ����
        if (fireDir == CharacterDirection.Left)
            transform.localScale = new Vector3(-1, 1, 1);
        isFire = true;
    }

    protected virtual void FixedUpdate()
    {
        //�߻� ����
        if (isFire && fireDir != 0)
        {
            Vector3 dir = new Vector3((int)fireDir, 0, 0) * speed;//�߻� ���� ���
            rBody.velocity = dir;//�Ѿ� �߻� ����
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(thisAnimation != null)
            thisAnimation.SetTrigger("Destroy");
            
        BattleManager _battleManager = BattleManager.Instance;
        _battleManager.GiveDamage(_battleManager.GetPlatformIndexForObj(other.gameObject), damage);//������ �ο�
        rBody.velocity = Vector3.zero;
        isFire = false;
        Invoke("ThisDestroy", destroyDelay);
    }

    private void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
