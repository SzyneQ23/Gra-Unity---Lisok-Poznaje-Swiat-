using UnityEngine;

public class ZlotaCzaszka : MonoBehaviour
{
    public AudioClip dzwiekWygranej; // Przeciągnij tu radosny dźwięk SFX
    public GameObject efektCząsteczkowy; // Opcjonalnie: iskry/błysk przy zebraniu

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy, czy to Lis (Player) dotknął dźwigni
        if (other.CompareTag("Player"))
        {
            PodniesPrzedmiot();
        }
    }

    void PodniesPrzedmiot()
    {
        // 1. Dźwięk wygranej
        if (dzwiekWygranej != null)
        {
            // Odtwarzamy dźwięk w miejscu, gdzie jest kamera, żeby był dobrze słyszalny
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