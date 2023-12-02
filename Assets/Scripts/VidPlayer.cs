using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    [SerializeField] private string videoFileName;
    private VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
    private bool isPlaying = false;

    void Update()
    {
        if(isPlaying==true){
            if (Input.anyKeyDown)
            {
                ChangeScene();
            }
            if (videoPlayer.isPaused && !videoPlayer.isPlaying)
        {
            ChangeScene();
        }

        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("MainMenue");
    }



    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            Debug.Log("Playing Video");
            isPlaying = true;
            
        }
    }
    

}

