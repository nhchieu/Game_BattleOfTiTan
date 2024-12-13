using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("# Player Info")]
    public Vector2 inputVec;
    public float speed = 7f;
    public float maxHealth = 100;
    public float Health;
    
    [Header("# Player Control")]
    float horizontal_input;
    float vertical_input;
    public Scanner scanner;
    SpriteRenderer spriter;
    Rigidbody2D rigid;
    Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter=GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner=GetComponent<Scanner>();
        Health=maxHealth;
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec =inputVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position+ nextVec); 
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (inputVec.magnitude > 0)
        {
            animator.SetFloat("Speed", inputVec.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", inputVec.magnitude);
        }
        if (inputVec.x != 0) { 
           spriter.flipX = inputVec.x < 0;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }
        Health -=Time.deltaTime* 10;

        if (Health < 0)
        {
            for (int i = 2; i < transform.childCount; i++) { 
                transform.GetChild(i).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }

    }
}
