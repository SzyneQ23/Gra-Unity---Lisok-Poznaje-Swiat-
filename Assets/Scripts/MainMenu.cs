using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void OnLevel1ButtonPressed()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void OnExitToDesktopButtonPressed()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
        #endif
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
