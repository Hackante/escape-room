using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayEntry : MonoBehaviour
{
    [SerializeField] private OneWayManager oneWayManager;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform destination;
    [SerializeField] private Transform wrongDestination;
    [SerializeField] private Animator crossFade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool? result = oneWayManager.Next(gameObject.name);
            if(result != null && result.Value) {
                crossFade.SetTrigger("Start");
                playerTransform.position = destination.position;
                crossFade.SetTrigger("End");
            } else if(result != null && !result.Value) {
                crossFade.SetTrigger("Start");
                playerTransform.position = wrongDestination.position;
                crossFade.SetTrigger("End");
            }
        }
    }
}
