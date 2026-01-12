using UnityEngine;

public class DarknessTrigger : MonoBehaviour
{
    public GameObject foxDarknessMask; 
    public bool disableOnEnter = true; 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foxDarknessMask.SetActive(!disableOnEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foxDarknessMask.SetActive(disableOnEnter);
        }
    }
}