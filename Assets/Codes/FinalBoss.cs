using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float health = 100f;
    public float speed = 25f;
    bool isLive = true;
    public Rigidbody2D target;
    SpriteRenderer spriter;
    Animator animator;
    Rigidbody2D rigid;
    Collider2D coll;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        
        
    }
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive || !isLive)
            return;
        
        Vector2 dirVec = target.position - rigid.position;

        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + nextVec);

        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("bosshit"))
        {
            return;
        }
    }

    private void LateUpdate()
    {
        //if (!GameManager.instance.isLive)
        //    return;

        //spriter.flipX = target.position.x < rigid.position.x;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Bullet") || !isLive)
    //    {
    //        return;
    //    }
        

    //    health -= collision.GetComponent<Bullet>().damage;
    //    //HUD
    //    GameManager.instance.Spawner.Hp -= collision.GetComponent<Bullet>().damage;

    //    if (health > 0)
    //    {
    //        animator.SetTrigger("bosshit");
    //    }
    //    else
    //    {
    //        isLive = false;
    //        coll.enabled = false;
    //        rigid.simulated = false;
    //        spriter.sortingOrder = 1;
    //        animator.SetBool("bossdead", true);
    //        StartCoroutine(Dead());
    //        GameManager.instance.kill++;
    //        GameManager.instance.GetExp();
    //        AudioManager.instance.sfx(1);
    //    }
    //}
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        GameManager.instance.GameWin();

    }

}
