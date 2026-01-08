using UnityEngine;

public class RegionInteraction : MonoBehaviour
{
    private Vector3 originalScale;
    public float hoverScaleMultiplier = 1.1f; 
    public Color hoverColor = Color.white;    
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        transform.localScale = originalScale * hoverScaleMultiplier;
        spriteRenderer.color = new Color(originalColor.r + 0.2f, originalColor.g + 0.2f, originalColor.b + 0.2f);
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        spriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        Debug.Log("Wybrano region: " + gameObject.name);
    }
}