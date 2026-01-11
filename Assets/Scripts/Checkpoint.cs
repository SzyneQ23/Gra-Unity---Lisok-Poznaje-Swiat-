using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerController player;
    public GameObject goldVfxPrefab;

    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goldVfxPrefab != null)
            {
                Instantiate(goldVfxPrefab, transform.position, Quaternion.identity);
            }
            player.SetRespawn(transform.position);

        }
    }


}
