using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Takes input from the player and moves the player character
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.1f;
    [SerializeField] ContactFilter2D movementFilter;

    // Movement
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Animations
    Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool moved = tryMove(movementInput);

            if (!moved)
            {
                // Try moving in the one direction
                Vector2 otherDirection = new Vector2(movementInput.x, 0);
                moved = tryMove(otherDirection);
                if (!moved)
                {
                    // Try moving in the other direction
                    otherDirection = new Vector2(0, movementInput.y);
                    moved = tryMove(otherDirection);
                }
            }
            // facing direction: 0 = south, 1 = east, 2 = north, 3 = west (unsing flipped sprite)
            int facingDirection = animator.GetInteger("facing");
            if (Mathf.Abs(movementInput.x) >= Mathf.Abs(movementInput.y))
            {
                if (movementInput.x > 0)
                {
                    facingDirection = 1;
                    spriteRenderer.flipX = false;
                }
                else
                {
                    facingDirection = 1;
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                spriteRenderer.flipX = false;
                if (movementInput.y > 0) facingDirection = 2;
                else facingDirection = 0;
            }
            animator.SetInteger("facing", facingDirection);
            animator.SetBool("isMoving", moved);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool tryMove(Vector2 direction)
    {
        if(direction == Vector2.zero) return false;
        // Check for potential collisions
        int count = rb.Cast(direction, movementFilter, castCollisions, movementSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void moveUp() {
        movementInput = new Vector2(0, 1);
    }

    public void moveDown() {
        movementInput = new Vector2(0, -1);
    }

    public void moveLeft() {
        movementInput = new Vector2(-1, 0);
    }

    public void moveRight() {
        movementInput = new Vector2(1, 0);
    }

    public void Move(Vector2 direction)
    {
        movementInput = direction;
    }
}
