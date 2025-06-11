using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float health = 5000f;
    public float speed = 15f;
    bool isLive = true;
    public Rigidbody2D target;
    SpriteRenderer spriter;
    Animator animator;
    Rigidbody2D rigid;
    Collider2D coll;
    Scanner scanner;
    public GameObject HealthBar;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        scanner = GetComponent<Scanner>();

    }
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(!GameManager.instance.isLive || !isLive)
            return;

        
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive || !isLive)
            return;
        Vector2 playerPos = scanner.nearestTarget.position;

        Vector2 dirVec = playerPos - rigid.position;

        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + nextVec);

        if (health <= 2500f)
        {
            animator.SetFloat("bossJump", dirVec.magnitude);

        }

    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }


        health -= collision.GetComponent<Bullet>().damage;
        //HUD
        GameManager.instance.Spawner.Hp -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            animator.SetTrigger("bosshit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            animator.SetBool("bossdead", true);
            StartCoroutine(Dead());
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
            AudioManager.instance.sfx(1);
        }
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        GameManager.instance.GameWin();

    }
}
