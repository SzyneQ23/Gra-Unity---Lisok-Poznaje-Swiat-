using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image goldIconPlatform;
    public Image goldIconLever; 
    public Image goldIconSkull;
    
    [Header("Ustawienia Koloru")]
    public Color activeColor = Color.yellow; 

    private void Awake()
    {
        instance = this;
    }

    public void ActivateGoldIconPlatform()
    {
        if (goldIconPlatform != null)
        {
            goldIconPlatform.color = activeColor;
            
            Debug.Log("HUD: Ikonka świeci teraz na żółto!");
        }
    }

     public void ActivateGoldIconLever()
    {
        if (goldIconLever != null)
        {
            goldIconLever.color = activeColor;
            
            Debug.Log("HUD: Ikonka świeci teraz na żółto!");
        }
    }

      public void ActivateGoldIconSkull()
    {
        if (goldIconSkull != null)
        {
            goldIconSkull.color = activeColor;
            
            Debug.Log("HUD: Ikonka świeci teraz na żółto!");
        }
    }
}