using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image goldIcon; 
    
    [Header("Ustawienia Koloru")]
    public Color activeColor = Color.yellow; 

    private void Awake()
    {
        instance = this;
    }

    public void ActivateGoldIcon()
    {
        if (goldIcon != null)
        {
            goldIcon.color = activeColor;
            
            Debug.Log("HUD: Ikonka świeci teraz na żółto!");
        }
    }
}