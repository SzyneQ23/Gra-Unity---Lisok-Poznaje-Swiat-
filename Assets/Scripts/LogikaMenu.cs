using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogikaMenu : MonoBehaviour
{
    public CanvasGroup panelPrzyciskowCG; 
    public CanvasGroup panelTytuluGryCG;
    public GameObject globus; 

    public float stopienPowiekszeniaGlobusa = 5.0f; 
    public float szybkoscZanikania = 2.0f;
    public string nazwaLevelu = "Level1"; 

    public void StartGry()
    {
      
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

      
        while (progress < stopienPowiekszeniaGlobusa)
        {
            progress += Time.deltaTime * szybkoscZanikania;

          
            if (panelPrzyciskowCG != null) panelPrzyciskowCG.alpha = Mathf.Clamp01(1 - (progress / 1.5f));
            if (panelTytuluGryCG != null) panelTytuluGryCG.alpha = Mathf.Clamp01(1 - (progress / 1.5f));

            if (globus != null)
            {
   
                globus.transform.localScale = Vector3.one * progress;
            }

            yield return null;
        }

     
        if (panelPrzyciskowCG != null) panelPrzyciskowCG.gameObject.SetActive(false);
        if (panelTytuluGryCG != null) panelTytuluGryCG.gameObject.SetActive(false);


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