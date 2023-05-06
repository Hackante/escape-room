using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private string sceneName;
    [SerializeField] private Animator animator;
    [SerializeField] private float delay = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
