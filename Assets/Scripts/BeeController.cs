using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] float speed;
    private Animator animator;
    private bool isFlying = false;
    private GameObject player;
    [SerializeField] float detectionRange = 5.0f;
    private bool isDead = false;
    public bool isFacingRight = false;
    private Vector3 startPosition;
    private Rigidbody2D rigidBody;
    

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator Kill()
    {
        if (!isDead)
        {
            isDead = true;
            isFlying = false;
            rigidBody.freezeRotation = false;
            rigidBody.gravityScale = 1;
            rigidBody.AddTorque(2.0f);
            Debug.Log("Pszczo³a unicestwiona");
            yield return new WaitForSeconds(0.3f);
            if (rigidBody.linearVelocityY <= 0.05)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (col.gameObject.transform.position.y > transform.position.y)
            {
                col.attachedRigidbody.linearVelocityY = 0;
                col.attachedRigidbody.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                StartCoroutine(Kill());
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isFlying = true;
        }

        else if (distanceToPlayer > detectionRange * 1.5f)
        {
            isFlying = false;
        }


        if (isFlying)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rigidBody.AddForce(direction * speed);
        }

        float currentX = transform.position.x;
        float playerX = player.transform.position.x;

        if (currentX < playerX)
            if (playerX > currentX && !isFacingRight)
            {
                Flip();
            }
        if (playerX < currentX && isFacingRight)
        {
            Flip();
        }
    }
}
