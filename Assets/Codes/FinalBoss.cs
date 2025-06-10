using System.ComponentModel;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float health = 100f;
    public float speed = 5f;
    bool isLive = true;
    public Transform target;
    SpriteRenderer spriter;
    [SerializeField] Animator animator;
    private Rigidbody2D rigid;
    Collider2D coll;
    Scanner scanner;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        target = GetComponentInParent<Scanner>().nearestTarget;
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        Vector2 dirVec = (Vector2)target.position - rigid.position;

        Vector2 nextVec = dirVec.normalized * speed * Time.deltaTime;

        rigid.MovePosition(rigid.position + nextVec);


    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        spriter.flipX = target.position.x < rigid.position.x;
    }
}
