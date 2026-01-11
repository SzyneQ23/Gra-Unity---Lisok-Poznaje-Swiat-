using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    [Header("Efekty")]
    public AudioClip goldSfx;           // Przeciągnij tu dźwięk podnoszenia
    public GameObject goldVfxPrefab;    // Przeciągnij tu prefab iskier

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 1. Logika gry
            GameManager.instance.hasGoldPlatform = true;
            UIManager.instance.ActivateGoldIconPlatform();

            // 2. Odtwarzanie dźwięku (PlayClipAtPoint nie wymaga AudioSource na obiekcie)
            if (goldSfx != null)
            {
                AudioSource.PlayClipAtPoint(goldSfx, transform.position);
            }

            // 3. Tworzenie cząsteczek
            if (goldVfxPrefab != null)
            {
                Instantiate(goldVfxPrefab, transform.position, Quaternion.identity);
            }

            // 4. Zniszczenie platformy
            Destroy(gameObject);
        }
    }
}