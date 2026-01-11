using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SlugController : MonoBehaviour
{
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    private Animator animator;
    private bool isDead = false;
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

    void Kill()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Slimak unicestwiony");
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isDead) return;

        if (col.CompareTag("Player"))
        {
            bool isFallingOnTop = col.attachedRigidbody.linearVelocity.y < 0 && col.transform.position.y > transform.position.y;

            if (isFallingOnTop)
            {
                col.attachedRigidbody.linearVelocity = new Vector2(col.attachedRigidbody.linearVelocity.x, 0);
                col.attachedRigidbody.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                Kill();
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

        if (isMovingRight) MoveRight(); else MoveLeft();

    }
}
