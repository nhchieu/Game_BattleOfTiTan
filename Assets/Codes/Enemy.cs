using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    [SerializeField] Rigidbody2D target;
    [SerializeField] bool islive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    WaitForFixedUpdate wait;
    Collider2D coll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        Vector2 dirVec = target.position - rigid.position;//vector chỉ hướng từ enemy đến player

        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;
        
        rigid.MovePosition(rigid.position + nextVec);
                                                     
        if (!islive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }
        
    }
    private void LateUpdate()
    {
        spriter.flipX = target.position.x<rigid.position.x;
    }

    void OnEnable()
    {
        target=GameManager.instance.player.GetComponent<Rigidbody2D>();
        islive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health =maxHealth;

    }

    public void Init(SpawnData data)
    {
       animator.runtimeAnimatorController= animCon[data.spriteType];
       speed=data.speed;
        maxHealth=data.health;
        health =data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }

        health-= collision.GetComponent<Bullet>().damage;
        KnockBack();

        if(health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            islive = false;
            coll.enabled = false;
            rigid.simulated= false;
            spriter.sortingOrder = 1;
            animator.SetBool("Dead",true);
            Dead();
            
        }
    }
        void KnockBack()
    {
        
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

    } 
    void Dead()
    {

        gameObject.SetActive(false);
    }
}
    
