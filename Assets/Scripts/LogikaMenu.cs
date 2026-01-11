using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogikaMenu : MonoBehaviour
{
    public CanvasGroup panelPrzyciskowCG; 
    public CanvasGroup panelTytuluGryCG;
    public GameObject globus; 

    public float stopienPowiekszeniaGlobusa = 5.0f; // Cel skali (np. 5)
    public float szybkoscZanikania = 2.0f;
    public string nazwaLevelu = "Level1"; // TU wpisz nazwę swojej sceny z grą

    public void StartGry()
    {
        // Jeśli globus był wyłączony, włączamy go i zerujemy skalę
        if (globus != null)
        {
            globus.SetActive(true);
            globus.transform.localScale = Vector3.zero; 
        }
        
        StartCoroutine(PlynnePrzejscie());
    }

    IEnumerator PlynnePrzejscie()
    {
        float progress = 0;

        // Pętla trwa, dopóki globus nie osiągnie docelowej skali
        while (progress < stopienPowiekszeniaGlobusa)
        {
            progress += Time.deltaTime * szybkoscZanikania;

            // Zapewniamy płynne znikanie UI (Clamp chroni przed wartościami ujemnymi)
            if (panelPrzyciskowCG != null) panelPrzyciskowCG.alpha = Mathf.Clamp01(1 - (progress / 1.5f));
            if (panelTytuluGryCG != null) panelTytuluGryCG.alpha = Mathf.Clamp01(1 - (progress / 1.5f));

            if (globus != null)
            {
                // Powiększanie globusa
                globus.transform.localScale = Vector3.one * progress;
            }

            yield return null;
        }

        // Gdy pętla się skończy (zoom gotowy):
        if (panelPrzyciskowCG != null) panelPrzyciskowCG.gameObject.SetActive(false);
        if (panelTytuluGryCG != null) panelTytuluGryCG.gameObject.SetActive(false);

        // --- KLUCZOWY MOMENT ---
        // Ładujemy scenę z grą
        SceneManager.LoadScene(nazwaLevelu);
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