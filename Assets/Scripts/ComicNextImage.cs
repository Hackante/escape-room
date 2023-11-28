using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicNextImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public Sprite[] images;
    private int currentIndex = 0;

    private float cameraMoveSpeed = 2f; // Adjust the speed as needed


    private void Update()
    {
        if (currentIndex == 14)
        {
            SceneManager.LoadScene("Elfendorf");
        }
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0)) return;
        ShowNextImage();

    }


    public void ShowNextImage()
    {
        currentIndex++;
        if (currentIndex == images.Length)
        {
            return;
        }
        // Change the shown image here using the currentIndex
        GetComponent<SpriteRenderer>().sprite = images[currentIndex];
        if (currentIndex == 3)
        {
            MoveCameraDown(-3.8f);
        }
        if (currentIndex == 5)
        {
            MoveCameraDown(-11.6f);
        }
        if (currentIndex == 8)
        {
            MoveCameraDown(-19.4f);
        }
        if (currentIndex == 11)
        {
            MoveCameraDown(-23.9f);
        }
        if (currentIndex == 13)
        {
            Vector3 StartingPosition = new Vector3(0, 30, -10);
            Camera.main.transform.position = StartingPosition;
            MoveCameraDown(2.7f);
        }

    }

    private void MoveCameraDown(float yValue)
    {
        // Calculate the target position for the camera
        Vector3 targetPosition = new Vector3(0, yValue, -10);

        // Smoothly move the camera towards the target position
        StartCoroutine(MoveCamera(targetPosition));
    }

    private IEnumerator MoveCamera(Vector3 targetPosition)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 startingPosition = Camera.main.transform.position;
        while (elapsedTime < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = targetPosition;
    }
}

