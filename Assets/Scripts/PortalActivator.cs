using UnityEngine;
using System.Collections;

public class PortalActivator : MonoBehaviour
{
    [Header("Grafiki")]
    public Sprite portalOffSprite;
    public Sprite portalOnSprite;
    
    [Header("Ustawienia")]
    public Transform destination; 
    public bool isActivated = false;
    public float freezeDuration = 0.5f;

    [Header("Dźwięki")]
    public AudioSource audioSource;
    public AudioClip activationSound;   
    public AudioClip teleportSound;

    private SpriteRenderer spriteRenderer;

   private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        UpdatePortalVisuals();
    }

    public void ActivatePortal()
    {
        if (!isActivated)
        {
            isActivated = true;
            UpdatePortalVisuals();
            

            if (audioSource && activationSound)
                audioSource.PlayOneShot(activationSound);

            Debug.Log("Portal aktywowany dźwiękiem!");
        }
    }

    private void UpdatePortalVisuals()
    {
        spriteRenderer.sprite = isActivated ? portalOnSprite : portalOffSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActivated && other.CompareTag("Player"))
        {
           
            if (audioSource && teleportSound)
                audioSource.PlayOneShot(teleportSound);

           
            StartCoroutine(TeleportWithFreeze(other.gameObject));
        }
    }

    private IEnumerator TeleportWithFreeze(GameObject player)
    {
        PlayerController controller = player.GetComponent<PlayerController>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (controller != null) controller.enabled = false;

        player.transform.position = destination.position;

        yield return new WaitForSeconds(freezeDuration);

        if (controller != null) controller.enabled = true;
        
        Debug.Log("Teleportacja zakończona, ruch przywrócony.");
    }
}