using UnityEngine;

public class SwitchSprites : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Switch()
    {
        spriteRenderer.sprite = sprite;
    }
}
