using UnityEngine;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [SerializeField] TMP_Text textLabel;
    [TextAreaAttribute(3, 10), SerializeField] private string text;

    private void Start()
    {
        GetComponent<TypewriterEffect>().Run(text, textLabel);
    }
}
