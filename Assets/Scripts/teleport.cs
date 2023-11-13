using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator animator;
    [SerializeField] private float delay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(delay/2);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(delay/2);
        SceneManager.LoadScene(sceneName);
    }

    public void SetSceneName(string sceneName)
    {
        this.sceneName = sceneName;
    }
}
