using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Singleton
    public static BackgroundMusic Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
            {
                Destroy(Instance.gameObject);
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}
