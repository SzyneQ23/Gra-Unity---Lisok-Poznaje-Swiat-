using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Potrzebne do obsługi płynnego zanikania

public class LogikaMenu : MonoBehaviour
{
    // Zmieniamy na CanvasGroup, aby móc sterować przezroczystością (Alpha)
    public CanvasGroup panelPrzyciskowCG; 
    public CanvasGroup panelTytuluGryCG;
    public GameObject globus;

    public float szybkoscZanikania = 2.0f; // Regulacja tempa (wyższa liczba = szybciej)

    public void StartGry()
    {
        // Najpierw włączamy globus w tle
        if (globus != null) globus.SetActive(true);
        
        // Rozpoczynamy proces płynnego znikania
        StartCoroutine(PlynneZnikanieMenu());
    }

    IEnumerator PlynneZnikanieMenu()
    {
        // Pętla wykonuje się, dopóki napisy są chociaż trochę widoczne
        while (panelPrzyciskowCG.alpha > 0 || panelTytuluGryCG.alpha > 0)
        {
            float zmiana = Time.deltaTime * szybkoscZanikania;
            
            // Zmniejszamy widoczność obu paneli
            panelPrzyciskowCG.alpha -= zmiana;
            panelTytuluGryCG.alpha -= zmiana;

            yield return null; // Czekaj na następną klatkę
        }

        // Gdy już całkiem znikną, wyłączamy obiekty całkowicie (dla optymalizacji)
        panelPrzyciskowCG.gameObject.SetActive(false);
        panelTytuluGryCG.gameObject.SetActive(false);
    }

    public void Wyjdz()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        Debug.Log("Gra zamknięta!");
    }
}