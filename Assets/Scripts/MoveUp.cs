using UnityEngine;

public class MoveUp : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    void Update()
    {
        transform.position = transform.position + Vector3.up * Time.deltaTime * moveSpeed;
    }
}
