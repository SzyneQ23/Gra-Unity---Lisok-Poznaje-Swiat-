using UnityEngine;

public class ZlotaDzwignia : MonoBehaviour
{
    public AudioClip dzwiekWygranej; 
    public GameObject efektCząsteczkowy; 
    public PortalActivator portal;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            portal.ActivatePortal(); 
            PodniesPrzedmiot();
        }
    }

    void PodniesPrzedmiot()
    {

        if (dzwiekWygranej != null)
        {
   
            AudioSource.PlayClipAtPoint(dzwiekWygranej, Camera.main.transform.position);
        }

 
        if (efektCząsteczkowy != null)
        {
            Instantiate(efektCząsteczkowy, transform.position, Quaternion.identity);
        }

        Debug.Log("Zdobycz zebrana! Gratulacje!");

        Destroy(gameObject);
    }
}