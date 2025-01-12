using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("# Player Info")]
    public Vector2 inputVec;
    public float speed = 7f;
    public float maxHealth = 100;
    public float Health;

    [Header("# Player Control")]
    private float horizontal_input;
    private float vertical_input;
    public Scanner scanner;
    private SpriteRenderer spriter;
    private Rigidbody2D rigid;
    private Animator animator;

    private bool isAlive => GameManager.instance.isLive;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        Health = maxHealth;
    }

    private void Update()
    {
        if (!isAlive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", inputVec.magnitude);
    }

    private void FixedUpdate()
    {
        if (!isAlive)
            return;

        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); 
    }

    private void LateUpdate()
    {
        if (!isAlive)
            return;

        if (inputVec.x != 0) 
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isAlive)
            return;

        if (Health > 0)
        {
            Health -= Time.deltaTime * 10;
        }

        if (Health <= 0)
        {
            for (int i = 2; i < transform.childCount; i++) 
            { 
                transform.GetChild(i).gameObject.SetActive(false);
            }

            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
