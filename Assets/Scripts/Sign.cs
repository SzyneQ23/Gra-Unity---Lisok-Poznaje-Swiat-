using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    public string message; 
    public TMP_Text uiTextElement; 
    public GameObject signCanvasElement; 

    private void OnTriggerEnter2D(Collider2D other)
    {
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