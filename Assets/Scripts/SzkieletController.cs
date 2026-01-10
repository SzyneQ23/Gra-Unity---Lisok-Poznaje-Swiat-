using UnityEngine;
using System.Collections;

public class SzkieletController : MonoBehaviour
{
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    private Animator animator;
    private bool isAwake = false;
    private bool isDead = false;
    public bool isFacingRight = false;

    float startPositionX;
    [SerializeField] private float moveRange = 1.0f;
    bool isMovingRight = false;

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
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


    public void WakeUp()
    {
        Debug.Log("Szkielet powstaje!");
        gameObject.SetActive(true);
        StartCoroutine(WakeUpAnimation());
    }

    IEnumerator WakeUpAnimation()
    {
        animator.SetBool("isAwake", true);
        yield return new WaitForSeconds(0.7f);
        isAwake=true;
    }
    IEnumerator KillOnAnimationEnd()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Szkielet unicestwiony");
            animator.SetBool("isDead", true);
            yield return new WaitForSeconds(0.4f);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.gameObject.transform.position.y > transform.position.y)
            {
                isAwake = false;
                col.attachedRigidbody.linearVelocityY = 0;
                col.attachedRigidbody.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }
    void Start()
    {
        
    }

    

    void Update()
    {

        float currentX = transform.position.x;

        if ((isMovingRight && currentX >= startPositionX + moveRange) ||
            (!isMovingRight && currentX <= startPositionX - moveRange))
        {
            isMovingRight = !isMovingRight;
            Flip();
        }
        if (isAwake)
        {
            if (isMovingRight) MoveRight(); else MoveLeft();
        }
    }
}
