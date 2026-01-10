using Cainos.PixelArtPlatformer_VillageProps;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0.01f, 20.0f)][SerializeField] private float moveSpeed = 0.1f;
    [Range(1.0f, 20.0f)][SerializeField] private float jumpForce = 6.0f;
    [Space(10)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float dashSpeed = 3.0f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private GameObject dashObject;
    private Rigidbody2D rigidBody;
    private BoxCollider2D coll;

    private Animator animator;
    private bool isRunning = false;
    private bool isDashing = false;
    private bool isFacingRight = true;
    private bool canDash = true;
    private TrailRenderer[] dashTrails;
    Vector2 startPosition;

    [Header("Darkness System")]
    public SpriteRenderer darknessOverlay; 
    public float fadeSpeed = 1.0f;
    private Coroutine fadeCoroutine;

    [Header("Building System")]
    [SerializeField] private GameObject ghostPreview; 
    [SerializeField] private float maxBuildDistance = 4.0f; 
    [SerializeField] private LayerMask blockedLayers;     
    private bool isBuildingMode = false;
    private bool isInCastleArea = false;

    public GameObject PlatformCounter;

    private void Awake()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        dashTrails = dashObject.GetComponentsInChildren<TrailRenderer>();
        
        if (ghostPreview) ghostPreview.SetActive(false);
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
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("jumping");
        }
    }

    IEnumerator Dash()
    {
        animator.SetBool("isRunning", true);
        canDash = false;
        isDashing = true;
        isRunning = true;
        animator.SetBool("isRunning", isRunning);
        float originalGravity = rigidBody.gravityScale;
        rigidBody.gravityScale = 0;
        rigidBody.linearVelocity = Vector2.zero;
        float direction = isFacingRight ? 1 : -1;
        float timer = dashDuration;
        UpdateTrailEmmiting(true);
        while (timer > 0)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
            timer -= Time.deltaTime;
            yield return null;
        }
        UpdateTrailEmmiting(false);
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("isRunning", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateTrailEmmiting(bool isEmitting)
    {
        foreach (TrailRenderer trail in dashTrails)
        {
            trail.emitting = isEmitting;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("ResetFace"))
        {
            BuildingManager.instance.ResetPlatforms();
        }

        if (col.CompareTag("LevelExit"))
        {
            if (GameManager.instance.keysCompleted == true)
            {
                GameManager.instance.AddPoints(100 * GameManager.instance.lives);
                GameManager.instance.LevelCompleted();
            }
            else Debug.Log("You need more keys to complete level");
        }
        if (col.CompareTag("LevelFall"))
        {
            transform.position = startPosition;
            GameManager.instance.AddLives(-1);
        }
        if (col.CompareTag("Bonus"))
        {
            GameManager.instance.AddPoints(3);
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("Enemy"))
        {
            if (transform.position.y > col.gameObject.transform.position.y) GameManager.instance.AddEnemiesKilled();
            else
            {
                transform.position = startPosition;
                GameManager.instance.AddLives(-1);
            }
        }
        if (col.CompareTag("Key"))
        {
            Diamonds diamond = col.GetComponent<Diamonds>();
            int keyNumber = diamond.keyID;
            GameManager.instance.AddKeys(keyNumber);
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("Heart"))
        {
            GameManager.instance.AddLives(1);
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("MovingPlatform"))
        {
            transform.SetParent(col.transform);
        }

        if (col.CompareTag("PlatformPickup"))
        {
            BuildingManager.instance.AddPlatforms(4);
            col.gameObject.SetActive(false);
        }
        if (col.CompareTag("Castle"))
        {
            BuildingManager.instance.availablePlatforms=0;
            BuildingManager.instance.availablePlatformsText.text=BuildingManager.instance.availablePlatforms.ToString();
            isInCastleArea = true;
            StartFade(1.0f);
            PlatformCounter.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("MovingPlatform")) transform.SetParent(null);
        if (col.CompareTag("Castle"))
        {
            PlatformCounter.SetActive(false);
            isInCastleArea = false;
            StartFade(0.0f); 
            BuildingManager.instance.ResetPlatforms();
            
            isBuildingMode = false;
            if (ghostPreview) ghostPreview.SetActive(false);
            BuildingManager.instance.availablePlatforms=0;
            BuildingManager.instance.availablePlatformsText.text=BuildingManager.instance.availablePlatforms.ToString();
        }
    }

    void Update()
    {
        if (GameManager.instance.currentGameState == GameState.GAME)
        {
            float verticalVelocity = rigidBody.linearVelocity.y;
            animator.SetFloat("verticalSpeed", verticalVelocity);

            if (Input.GetKeyDown(KeyCode.LeftAlt) && canDash && !isDashing)
            {
                StartCoroutine(Dash());
            }

            if (isDashing) return;

            isRunning = false;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isRunning = true;
                if (!isFacingRight) { Flip(); isFacingRight = true; }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(-moveSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                isRunning = true;
                if (isFacingRight) { Flip(); isFacingRight = false; }
            }

            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && !isBuildingMode))
            {
                Jump();
            }

            animator.SetBool("isGrounded", IsGrounded());
            animator.SetBool("isRunning", isRunning);

            if (Input.GetKeyDown(KeyCode.C))
            {
                isBuildingMode = !isBuildingMode;
                if (ghostPreview) ghostPreview.SetActive(isBuildingMode);
            }

            if (isBuildingMode)
            {
                HandleBuilding();
            }
        }
    }

    void HandleBuilding()
    {
        if (!isInCastleArea) 
        {
            if (ghostPreview) ghostPreview.SetActive(false);
            return;
        }
 
        Vector3 rawMousePos = Input.mousePosition;
        rawMousePos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(rawMousePos);
        worldPos.z = 0f;

        if (ghostPreview) ghostPreview.transform.position = worldPos;

        float dist = Vector2.Distance(transform.position, worldPos);
        
        Vector2 platformSize = BuildingManager.instance.platformPrefab.GetComponent<BoxCollider2D>().size;
        Collider2D overlap = Physics2D.OverlapBox(worldPos, platformSize * 0.8f, 0f, blockedLayers);
        
        bool canPlace = (dist <= maxBuildDistance) && (overlap == null) && (BuildingManager.instance.availablePlatforms > 0);

        SpriteRenderer gs = ghostPreview.GetComponent<SpriteRenderer>();
        if (gs) gs.color = canPlace ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);

        if (Input.GetMouseButtonDown(0) && canPlace)
        {
            BuildingManager.instance.PlacePlatform(worldPos);
        }
    }

    private void StartFade(float targetAlpha)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        if (darknessOverlay == null) yield break;
        Color color = darknessOverlay.color;
        float startAlpha = color.a;
        float timer = 0;
        while (timer < fadeSpeed)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeSpeed);
            darknessOverlay.color = color;
            yield return null;
        }
    }
}