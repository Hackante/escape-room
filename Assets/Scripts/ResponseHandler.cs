using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogUI;

    List<GameObject> responseButtons = new List<GameObject>();

    private void Start()
    {
        dialogUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses) {
        float responseBoxHeight = 0f;

        foreach (Response response in responses) {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);

            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => {
                OnPickedResponse(response);
            });

            responseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response) {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject responseButton in responseButtons) {
            Destroy(responseButton);
        }
        responseButtons.Clear();
        dialogUI.ShowDialogue(response.DialogueObject);
    }
}
