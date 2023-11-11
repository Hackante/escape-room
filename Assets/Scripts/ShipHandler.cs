using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO: below
- Add Dialogue after impact
- Add Quest
- Add Other Images for the Quest
*/

public class ShipHandler : MonoBehaviour
{
    [SerializeField] private float spawnOctopusTimer = 5f;
    [SerializeField] private GameObject Octopus;
    [SerializeField] private GameObject ImpactPoint;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;

    private void Start()
    {
        StartCoroutine(SpawnOctopus());
    }

    private IEnumerator SpawnOctopus()
    {
        yield return new WaitForSeconds(spawnOctopusTimer);
        Octopus.SetActive(true);
        Octopus.GetComponent<Animator>().SetTrigger("Spawn");
        MoveOctopusToShip();
    }

    private void MoveOctopusToShip()
    {
        // Slowly move the Octopus to the ship
        StartCoroutine(MoveOctopus());
    }

    private IEnumerator MoveOctopus()
    {
        // Morph the Octopus to the ship till it is at this gameobjects transformposition
        float morphTime = 1f;
        float elapsedTime = 0f;
        Vector2 startPosition = Octopus.transform.position;

        while (elapsedTime < morphTime)
        {
            Octopus.transform.position = Vector2.Lerp(startPosition, (Vector2)ImpactPoint.transform.position, elapsedTime / morphTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraShake.ShakeCamera();
        // TODO: add dialogue
        dialogueUI.ShowDialogue(dialogueObject);
    }
}
