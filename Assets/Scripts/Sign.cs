using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public string message; // Tutaj wpiszesz treść w Inspektorze
    public TMP_Text uiTextElement; // Przeciągnij tutaj swój SignText
    public GameObject signCanvasElement; // Opcjonalnie: cały obiekt tła tekstu

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdź, czy to gracz wszedł w tabliczkę
        if (other.CompareTag("Player"))
        {
            uiTextElement.text = message;
            uiTextElement.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiTextElement.gameObject.SetActive(false);
        }
    }
}