using UnityEngine;

public class GoldenLever : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.hasGoldLever = true;

            UIManager.instance.ActivateGoldIconLever();

            Destroy(gameObject);
        }
    }
}

