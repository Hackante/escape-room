using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private float spawnOctopusTimer = 1f;
    [SerializeField] private GameObject Octopus;
    [SerializeField] private GameObject ImpactPoint;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObjectStart;
    [SerializeField] private DialogueObject dialogueObjectEnd;
    [SerializeField] private UnityEvent[] closeEvents;
    

    private void Start()
    {
        //StartCoroutine(SpawnOctopus());
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
        StartCannons();
    }

    public void DespawnOctopus()
    {
        Octopus.GetComponent<Animator>().SetTrigger("Despawn");
        StopCannons();
        // Deactivate own Collider
        GetComponent<Collider2D>().enabled = false;
    }

    private void StartCannons()
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
        foreach (GameObject cannon in cannons)
        {
            StartCoroutine(SetCannonsShootingTo(true, cannon.GetComponent<Animator>()));
        }
    }

    private void StopCannons()
    {
        GameObject[] cannons = GameObject.FindGameObjectsWithTag("Cannon");
        foreach (GameObject cannon in cannons)
        {
            cannon.GetComponent<Animator>().SetBool("IsShooting", false);
        }
    }

    private IEnumerator SetCannonsShootingTo(bool isShooting, Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        animator.SetBool("IsShooting", isShooting);
    }

    public void Interact(PlayerController playerController)
    {
        // Start Dialogue
        dialogueUI.ShowDialogue(dialogueObjectStart);
        // Spawn Octopus
        StartCoroutine(SpawnOctopus());
        foreach (UnityEvent closeEvent in closeEvents)
        {
            dialogueUI.AddCloseEvent(closeEvent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.Interactable = this;
            UI.Instance.SetInteractBttnInteractable(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            if ((object)playerController.Interactable == (object)this)
            {
                playerController.Interactable = null;
                UI.Instance.SetInteractBttnInteractable(false);
            }
        }
    }
}
