using UnityEngine;

public class PointItem : MonoBehaviour
{
    [Header("Ustawienia Ruchu")]
    public float amplituda = 0.5f;
    public float czestotliwosc = 1f;

    [Header("Ustawienia Dźwięku")]
    public AudioClip dzwiekZebrania;
    [Range(0f, 1f)] public float glosnosc = 0.5f; 

    private Vector3 pozycjaStartowa;

    void Start()
    {
        pozycjaStartowa = transform.position;
    }

    void Update()
    {
        float nowaWysokosc = Mathf.Sin(Time.time * czestotliwosc) * amplituda;
        transform.position = pozycjaStartowa + new Vector3(0, nowaWysokosc, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ZebierzPunkt();
        }
    }

    void ZebierzPunkt()
    {
        if (dzwiekZebrania != null)
        {
            
            AudioSource.PlayClipAtPoint(dzwiekZebrania, Camera.main.transform.position, glosnosc);
        }

        Debug.Log("Punkt zebrany!");
        Destroy(gameObject);
    }
}