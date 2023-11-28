using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingGame : MonoBehaviour
{
    [SerializeField] private RectTransform staticBar;
    [SerializeField] private RectTransform movingBar;
    [SerializeField] private RectTransform area;
    [SerializeField] private GameObject footer;
    [SerializeField] private ReactionGameQuestUI reactionGameQuestUI;

    [SerializeField] private float speed = 1f;
    private int score = 0;

    private void OnEnable()
    {
        StartCoroutine(MoveBar());
    }

    // The moving bar should bounce between the two ends on the x axis of the staticBar with a constant speed.
    private IEnumerator MoveBar()
    {
        bool isMovingRight = true;
        while (true)
        {
            if (isMovingRight)
            {
                movingBar.anchoredPosition += Vector2.right * speed;
                if (movingBar.anchoredPosition.x >= staticBar.rect.width / 2)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                movingBar.anchoredPosition -= Vector2.right * speed;
                if (movingBar.anchoredPosition.x <= -staticBar.rect.width / 2)
                {
                    isMovingRight = true;
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    // The player can click a button when the player thinks the bar is in the area recttransform.
    // If the player is right, the game is won.
    private void Update()
    {
        if(!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0)) return;
        if (IsInArea())
        {
            if(score == 4) {
                footer.SetActive(true);
                reactionGameQuestUI.OnConfirm();
            }
            else {
                score++;
                // Shrink the area recttransform width to 75% of the original width.
                area.sizeDelta = new Vector2(area.rect.width * 0.75f, area.rect.height);
                // double the speed of the moving bar.
                speed *= 2;
            }
        }
        else
        {
            score = 0;
            // Reset the area recttransform width to the original width.
            area.sizeDelta = new Vector2(400f, area.rect.height);
            // Reset the speed of the moving bar.
            speed = 1f;
        }
    }

    private bool IsInArea()
    {
        return movingBar.anchoredPosition.x >= area.anchoredPosition.x - area.rect.width / 2 &&
               movingBar.anchoredPosition.x <= area.anchoredPosition.x + area.rect.width / 2;
    }
}
