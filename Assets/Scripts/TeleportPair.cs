using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPair : MonoBehaviour
{
    public bool IsAvtive = true;
    [SerializeField] private List<GameObject> tpPoints = new List<GameObject>();
    [SerializeField] private Animator crossfadeAnimator;
    [SerializeField] private float delay = 1f;

    public void TeleportFrom(Transform transform)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (IsAvtive && player != null)
        {
            crossfadeAnimator.SetTrigger("Start");
            StartCoroutine(teleportTo(transform));
        }
    }

    private IEnumerator teleportTo(Transform transform)
    {
        GameObject player = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(delay);
        if (transform == tpPoints[0].transform)
        {
            tpPoints[1].GetComponent<TeleportPoint>().IsAvtive = false;
            player.transform.position = tpPoints[1].transform.position;
        }
        else
        {
            tpPoints[0].GetComponent<TeleportPoint>().IsAvtive = false;
            player.transform.position = tpPoints[0].transform.position;
        }
        crossfadeAnimator.SetTrigger("End");
    }
}
