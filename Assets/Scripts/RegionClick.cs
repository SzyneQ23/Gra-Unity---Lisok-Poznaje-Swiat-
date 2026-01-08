using UnityEngine;
using UnityEngine.SceneManagement; 

public class RegionClick : MonoBehaviour
{
    public string nazwaSceny = "Level Architektura";

    private void OnMouseDown()
    {
        Debug.Log("Lecimy do poziomu: " + nazwaSceny);
        SceneManager.LoadScene(nazwaSceny);
    }
}