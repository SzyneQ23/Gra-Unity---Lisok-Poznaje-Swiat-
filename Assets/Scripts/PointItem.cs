using UnityEngine;

public class PointItem : MonoBehaviour
{
    [Header("Ustawienia Ruchu")]
    public float amplituda = 0.5f; // Jak wysoko i nisko ma latać
    public float czestotliwosc = 1f; // Jak szybko ma się poruszać

    [Header("Ustawienia Dźwięku")]
    public AudioClip dzwiekZebrania;

    private Vector3 pozycjaStartowa;

    void Start()
    {
        // Zapamiętujemy miejsce, w którym postawiliśmy przedmiot
        pozycjaStartowa = transform.position;
    }

    void Update()
    {
        // Efekt lewitacji (używamy funkcji Sinus do płynnego ruchu)
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
        // Odtwarzamy dźwięk w miejscu kamery (żeby był wyraźny nawet po zniszczeniu obiektu)
        if (dzwiekZebrania != null)
        {
            AudioSource.PlayClipAtPoint(dzwiekZebrania, Camera.main.transform.position);
        }

        // Tutaj dodaj swoją logikę punktów, np.:
        // GameManager.instance.DodajPunkt(1);

        Debug.Log("Punkt zebrany!");
        Destroy(gameObject);
    }
}