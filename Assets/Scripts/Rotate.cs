using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float rotation;

    public void RotateObject() {
        rectTransform.Rotate(0, 0, rotation);
    }

    public void RotateObject(float rotation) {
        rectTransform.Rotate(0, 0, rotation);
    }
}
