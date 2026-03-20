using UnityEngine;

public class GoldenSkull : MonoBehaviour
{
    public PortalActivator portal;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.instance.hasGoldSkull = true;
            portal.ActivatePortal(); 
            UIManager.instance.ActivateGoldIconSkull();

            Destroy(gameObject);
        }
    }
}

