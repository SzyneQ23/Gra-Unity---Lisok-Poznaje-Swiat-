using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    [Header("Efekty")]
    public AudioClip goldSfx;     
    public GameObject goldVfxPrefab;  

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.hasGoldPlatform = true;
            UIManager.instance.ActivateGoldIconPlatform();

            if (goldSfx != null)
            {
                AudioSource.PlayClipAtPoint(goldSfx, transform.position);
            }

            if (goldVfxPrefab != null)
            {
                Instantiate(goldVfxPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}