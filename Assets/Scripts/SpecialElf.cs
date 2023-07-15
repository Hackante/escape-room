using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class specialElf : MonoBehaviour
{
    private bool onRope = false;
    private Animator anim;
    [SerializeField] private GameObject rope;

    void Start()
    {
        anim = GetComponent<Animator>();
        this.OnRope = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("UI/InteractBttn").GetComponent<Button>().interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("UI/InteractBttn").GetComponent<Button>().interactable = false;
        }
    }

    public bool OnRope
    {
        get { return onRope; }
        set
        {
            onRope = value;
            rope.SetActive(value);
        }
    }

    public bool isSpinning
    {
        get { return anim.GetBool("isSpinning"); }
        set { anim.SetBool("isSpinning", value); }
    }

    public void spin()
    {
        isSpinning = true;
    }

    public void idle()
    {
        isSpinning = false;
    }

    public void flyInside(Vector2 pos)
    {
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float y = cam.transform.position.y + cameraHeight / 2f;
        OnRope = true;
        isSpinning = true;
        transform.position = new Vector2(pos.x, y + 0.2f);
        StartCoroutine(flyCoroutine(pos));
    }

    public void flyOutside()
    {
        Camera cam = Camera.main;
        float cameraHeight = 2f * cam.orthographicSize;
        float y = cam.transform.position.y + cameraHeight / 2f;
        OnRope = true;
        anim.SetBool("isSpinning", true);
        StartCoroutine(flyCoroutine(new Vector2(transform.position.x, y + 0.2f)));
    }

    private IEnumerator flyCoroutine(Vector2 pos)
    {
        float morphTime = 1f;
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        while (elapsedTime < morphTime)
        {
            transform.position = Vector2.Lerp(startPosition, pos, elapsedTime / morphTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
    }
}