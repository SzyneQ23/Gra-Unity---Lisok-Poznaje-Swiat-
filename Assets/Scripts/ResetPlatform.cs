using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            BuildingManager.instance.ResetPlatforms();
        }
    }
}