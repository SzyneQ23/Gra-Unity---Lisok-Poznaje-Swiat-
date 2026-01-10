using UnityEngine;
using System.Collections;
using UnityEditor;

public class MagController : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    public bool isFacingRight = false;
    private GameObject player;
    [SerializeField] float throwTimer = 3.0f;
    [SerializeField] FIreballController fireballPrefab;
    [SerializeField] GameObject fireballSpawn;
    private float timer;
    private bool throwing = false;

    float startPositionX;

    private void Awake()
    {
        startPositionX = this.transform.position.x;
        animator = GetComponent<Animator>();
        timer = throwTimer + 2.0f;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator KillOnAnimationEnd()
    {
        if (!isDead)
        {
            isDead = true;
            Debug.Log("Mag unicestwiony");
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
                col.attachedRigidbody.linearVelocityY = 0;
                col.attachedRigidbody.AddForce(Vector2.up * 3.0f, ForceMode2D.Impulse);
                StartCoroutine(KillOnAnimationEnd());
            }
        }
    }

    IEnumerator Throw()
    {
        animator.SetBool("throw", true);
        yield return new WaitForSeconds(0.6f);
        Instantiate(fireballPrefab, fireballSpawn.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("throw", false);
    }

    public void activateThrow()
    {
        throwing = true;
        timer = throwTimer;
    }

    public void disableThrow()
    {
        throwing = false;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    void Update()
    {

        if (throwing)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                StartCoroutine(Throw());
                timer = throwTimer;
            }
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
