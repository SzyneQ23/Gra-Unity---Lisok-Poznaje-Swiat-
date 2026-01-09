using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // 1. Zapisujemy w GameManagerze, że mamy przedmiot
            GameManager.instance.hasGoldPlatform = true;

            // 2. Zapalamy ikonkę w UI
            UIManager.instance.ActivateGoldIcon();

            // 3. Usuwamy przedmiot ze świata
            Destroy(gameObject);
        }
    }
}

