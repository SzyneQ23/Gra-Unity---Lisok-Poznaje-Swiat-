using UnityEngine;

public class RegionInteraction : MonoBehaviour
{
    private Vector3 originalScale;
    public float hoverScaleMultiplier = 1.1f; // O ile się powiększy po najechaniu
    public Color hoverColor = Color.white;    // Kolor po najechaniu (opcjonalnie)
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Wykrywa najechanie myszką
    void OnMouseEnter()
    {
        transform.localScale = originalScale * hoverScaleMultiplier;
        // Opcjonalnie: rozjaśnienie regionu
        spriteRenderer.color = new Color(originalColor.r + 0.2f, originalColor.g + 0.2f, originalColor.b + 0.2f);
    }

    // Wykrywa zjechanie myszką
    void OnMouseExit()
    {
        transform.localScale = originalScale;
        spriteRenderer.color = originalColor;
    }

    // Wykrywa kliknięcie
    void OnMouseDown()
    {
        Debug.Log("Wybrano region: " + gameObject.name);
        // Tutaj dodamy kod ładowania sceny (np. SceneManager.LoadScene("Poziom1");)
    }
}