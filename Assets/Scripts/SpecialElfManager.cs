using UnityEngine;

public class SpecialElfManager : MonoBehaviour
{
    [SerializeField] private GameObject specialElf;

    private void Start()
    {
        specialElf.SetActive(false);
    }

    public void flyTo(Vector2 pos)
    {
        specialElf.SetActive(true);
        specialElf.GetComponent<specialElf>().flyInside(pos);
    }
}
