using UnityEngine;

public class GoldenSkull : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.hasGoldSkull = true;

            UIManager.instance.ActivateGoldIconSkull();

            Destroy(gameObject);
        }
    }
}

