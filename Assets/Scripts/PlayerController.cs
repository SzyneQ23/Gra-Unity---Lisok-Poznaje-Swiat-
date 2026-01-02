using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    [Range(1.0f, 20.0f)] [SerializeField] private float jumpForce = 6.0f;
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    const float rayLength = 0.2f;
    private Rigidbody2D rigidBody;

    private Animator animator;
    private bool isRunning = false;
    private bool isFacingRight=true;
    Vector2 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("jumping");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("LevelExit"))
        {
            if (GameManager.instance.keysCompleted==true)
            {
                GameManager.instance.AddPoints(100*GameManager.instance.lives);
                GameManager.instance.LevelCompleted();
            }
            else
            {
                Debug.Log("You need more keys to complete level");
            }
            
        }
        if (col.CompareTag("LevelFall"))
        {
            transform.position = startPosition;
            GameManager.instance.AddLives(-1);
        }
        if(col.CompareTag("Bonus"))
        {
            GameManager.instance.AddPoints(3);
            col.gameObject.SetActive(false);
        }
        if(col.CompareTag("Enemy"))
        {
            if (transform.position.y > col.gameObject.transform.position.y)
            {
                GameManager.instance.AddEnemiesKilled();
            }
            else
            {
                transform.position = startPosition;
                GameManager.instance.AddLives(-1);
            }
        }
        if (col.CompareTag("Key"))
        {
            Diamonds diamond = col.GetComponent<Diamonds>();
            int keyNumber=diamond.keyID;
            GameManager.instance.AddKeys(keyNumber);
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("Heart"))
        {
            GameManager.instance.AddLives(1);
            col.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        
    }
    void Update()
    {   
        if (GameManager.instance.currentGameState==GameState.GAME)
        {
            isRunning = false;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isRunning = true;
                if (isFacingRight==false)
                {
                    Flip();
                    isFacingRight = true;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isRunning = true;

                if (isFacingRight == true)
                {
                    Flip();
                    isFacingRight = false;
                }
            }
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 0.2f, false);
            animator.SetBool("isGrounded", IsGrounded());
            animator.SetBool("isRunning", isRunning);
        }
    }
}
