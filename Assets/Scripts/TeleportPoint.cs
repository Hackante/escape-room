using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public bool IsAvtive = true;
    [SerializeField] private GameObject teleportPairManager;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && IsAvtive) {
            teleportPairManager.GetComponent<TeleportPair>().TeleportFrom(transform);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            IsAvtive = true;
        }
    }
}
