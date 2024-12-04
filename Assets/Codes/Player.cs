using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector2 inputVec;
    [SerializeField] float speed = 4f;
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
    }
    void Update()
    {
        //// Lấy input từ bàn phím
        //horizontal_input = Input.GetAxisRaw("Horizontal"); // A (-1) / D (1)
        //vertical_input = Input.GetAxisRaw("Vertical"); // W (1) / S (-1)
        //// Tạo vector di chuyển trên mặt phẳng X0Y
        //Vector3 moveDirection = new Vector3(horizontal_input, vertical_input, 0f).normalized;
        //// Di chuyển nhân vật
        //transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");




    }
    private void FixedUpdate()
    {
        Vector2 nextVec =inputVec.normalized*speed*Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position+ nextVec); 

    }

    private void LateUpdate()
    {
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
}
