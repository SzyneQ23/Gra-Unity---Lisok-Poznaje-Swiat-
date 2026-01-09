using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image goldIcon; // Twoja ikonka z Sunny Land
    
    [Header("Ustawienia Koloru")]
    public Color activeColor = Color.yellow; // Kolor, który ma się pojawić po zebraniu

    private void Awake()
    {
        instance = this;
    }

    public void ActivateGoldIcon()
    {
        if (goldIcon != null)
        {
            // Zamiast Color.white (który resetuje do oryginału),
            // ustawiamy Twój wybrany żółty kolor z pełnym kanałem Alpha
            goldIcon.color = activeColor;
            
            Debug.Log("HUD: Ikonka świeci teraz na żółto!");
        }
    }
}