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
    private ResponseEvent[] responseEvents;

    List<GameObject> responseButtons = new List<GameObject>();

    private void Start()
    {
        dialogUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);

            responseButton.GetComponentInChildren<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnPickedResponse(response, responseIndex);
            });

            responseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject responseButton in responseButtons)
        {
            Destroy(responseButton);
        }
        responseButtons.Clear();
        if (responseEvents[responseIndex] != null && responseIndex <= responseEvents.Length)
            responseEvents[responseIndex].OnPickedResponse?.Invoke();

        responseEvents = null;

        if (response.DialogueObject != null)
            dialogUI.ShowDialogue(response.DialogueObject);
        else dialogUI.CloseDialogue();
    }
}
