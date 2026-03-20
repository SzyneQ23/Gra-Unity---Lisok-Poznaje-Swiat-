using UnityEngine;

public class MagTrigger : MonoBehaviour
{
    public MagController mag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mag rzuca fireballe!");
            mag.activateThrow();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mag przestaje rzucac fireballami!");
            mag.disableThrow();
        }
    }
}
