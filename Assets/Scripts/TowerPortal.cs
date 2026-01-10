using UnityEngine;

public class TowerPortal : MonoBehaviour
{
    [Header("0=zolty, 1=Czerwony, 2=Niebieski, 3=Zielony")]
    public int colorID; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            TowerPortalManager.instance.PortalEntered(colorID, col.gameObject);
        }
    }
}