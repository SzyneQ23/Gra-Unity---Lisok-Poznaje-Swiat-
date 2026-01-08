using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogikaMenu : MonoBehaviour
{
    public CanvasGroup panelPrzyciskowCG; 
    public CanvasGroup panelTytuluGryCG;
    public GameObject globus; // To Twój Globus_Base

    public float stopienPowiekszeniaGlobusa;

    public float szybkoscZanikania = 2.0f;

    public void StartGry()
    {
        if (globus != null)
        {
            globus.SetActive(true);
            // Ustawiamy skalę globusa na 0, żeby był niewidoczny na starcie
            globus.transform.localScale = Vector3.zero; 
        }
        
        StartCoroutine(PlynnePrzejscie());
    }

    IEnumerator PlynnePrzejscie()
    {
        float progress = 0;

        while (progress < stopienPowiekszeniaGlobusa)
        {
            progress += Time.deltaTime * szybkoscZanikania;

            // 1. Zanikanie UI
            panelPrzyciskowCG.alpha = 1 - progress;
            panelTytuluGryCG.alpha = 1 - progress;

            // 2. Płynne powiększanie globusa (od 0 do 1)
            if (globus != null)
            {
                globus.transform.localScale = Vector3.one * progress;
            }

            yield return null;
        }

        // Finalne wyłączenie UI
        panelPrzyciskowCG.gameObject.SetActive(false);
        panelTytuluGryCG.gameObject.SetActive(false);
    }
    
    // ... reszta kodu (Wyjdz itp.)

    public void Wyjdz()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        Debug.Log("Gra zamknięta!");
    }
}