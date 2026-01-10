using UnityEngine;

public class GoldPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.hasGoldPlatform = true;

            UIManager.instance.ActivateGoldIcon();

            Destroy(gameObject);
        }
    }
}

