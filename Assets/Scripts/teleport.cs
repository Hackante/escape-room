using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator animator;
    [SerializeField] private float delay = 1f;
    [SerializeField] private bool InstantStart = false;

    private void Start()
    {
        if (InstantStart)
        {
            StartCoroutine(LoadSceneWithoutWaiting());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        SavePosition();
        yield return new WaitForSeconds(delay / 2);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(delay / 2);
        SceneManager.LoadScene(sceneName);
    }

    public void SetSceneName(string sceneName)
    {
        this.sceneName = sceneName;
    }

    public void SetSceneName(int sceneIndex)
    {
        string sceneName = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
        this.sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);
    }

    private IEnumerator LoadSceneWithoutWaiting()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private void SavePosition()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Elfendorf":
                SaveObject.Instance.elfendorfPosition = new Vector3(6.55f, -21f);
                break;
            case "Ocean":
                SaveObject.Instance.ozeanPosition = new Vector3(5.6f, -23.6f);
                break;
        }
    }
}
