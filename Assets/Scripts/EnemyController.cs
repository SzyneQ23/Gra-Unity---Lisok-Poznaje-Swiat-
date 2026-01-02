using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Range(0.01f, 20.0f)] [SerializeField] private float moveSpeed = 0.1f;
    private Animator animator;
    public bool isFacingRight = false;

    float startPositionX;
    [SerializeField] private float moveRange = 1.0f;
    bool isMovingRight = false;

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        animator = GetComponent<Animator>();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void MoveRight()
    {
        transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
    }

    IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.gameObject.transform.position.y > transform.position.y)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }
    void Start()
    {}

    void Update()
    {
        if (isMovingRight==true)
        {
            if (this.transform.position.x < startPositionX + moveRange)
            {
                MoveRight();
            }
            else
            {
                Flip();
                MoveLeft();
                isMovingRight = false;
            }
        }
        else
        {
            if (this.transform.position.x > startPositionX - moveRange)
            {
                MoveLeft();
            }
            else
            {
                Flip();
                MoveRight();
                isMovingRight = true;
            }
        }
    }
}
