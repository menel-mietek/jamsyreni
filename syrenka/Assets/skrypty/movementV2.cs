using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementV2 : MonoBehaviour
{
    public float MoveSpeed = 5f;  // Speed of player movement
    public Transform movepoint;   // The point that the player moves towards (should snap to tiles)
    public LayerMask whatStopsMovement;  // Layer for obstacles like walls
    public LayerMask pushableLayer;  // Layer for pushable objects
    public float tileSize = 1f;    // Size of each tile (1x1 units)
    [SerializeField] Rigidbody2D rb;
    private Animator animator;     // Reference to the Animator on a different object
    private Vector2 lastMoveDirection = Vector2.zero;  // Keep track of the last horizontal movement direction

    public GameObject animatorObject; // Reference to the GameObject holding the Animator

    void Start()
    {
        movepoint.parent = null;  // Ensure the movepoint is not parented to the player
        movepoint.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);  // Snap to grid

        // Get the Animator from the assigned object (the object with the Animator)
        if (animatorObject != null)
        {
            animator = animatorObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Animator object is not assigned!");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = getVector() * MoveSpeed;

        // Check if the player is at the movepoint
        if (Vector3.Distance(transform.position, movepoint.position) < 0.05f)
        {
            // Horizontal movement
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                TryMove(moveDirection);
            }
            // Vertical movement
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                Vector3 moveDirection = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                TryMove(moveDirection);
            }
            else
            {
                // If there's no input, player is idle
                SetIdleAnimation();
            }
        }

        // Update the IsMoving parameter
        UpdateIsMoving();
    }

    // Method to try and move the player and interact with pushable objects
    void TryMove(Vector3 moveDirection)
    {
        // Check if the new position is blocked by an obstacle
        Vector3 newPlayerPosition = movepoint.position + moveDirection * tileSize;

        if (!Physics2D.OverlapCircle(newPlayerPosition, 0.1f, whatStopsMovement))
        {
            // Check if there is a pushable object in the way
            Collider2D pushableObjectCollider = Physics2D.OverlapCircle(newPlayerPosition, 0.1f, pushableLayer);

            if (pushableObjectCollider != null)
            {
                // Try to push the object in the desired direction
                push pushableObject = pushableObjectCollider.GetComponent<push>();
                if (pushableObject != null)
                {
                    if (pushableObject.TryToPush(moveDirection))
                    {
                        // If the object was successfully pushed, move the player
                        movepoint.position = SnapToTile(newPlayerPosition);
                    }
                }
            }
            else
            {
                // If no pushable object is in the way, move the player
                movepoint.position = SnapToTile(newPlayerPosition);
            }

            // Update animation for movement
            UpdateMovementAnimation(moveDirection);
        }
    }

    Vector2 getVector()
    {
        if ((movepoint.position - transform.position).magnitude > .04f)
            return (movepoint.position - transform.position).normalized;
        else return Vector2.zero;
    }

    // Method to snap any position to the nearest tile
    Vector3 SnapToTile(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0f);
    }

    // Update movement animation based on direction
    void UpdateMovementAnimation(Vector3 moveDirection)
    {
        if (animator != null)
        {
            // Update the movement parameters in the Animator
            animator.SetFloat("MoveX", moveDirection.x);
            animator.SetFloat("MoveY", moveDirection.y);

            // Keep track of the last movement direction (only horizontal)
            if (moveDirection.x != 0)
            {
                lastMoveDirection = new Vector2(moveDirection.x, 0f);
                animator.SetFloat("LastMoveX", moveDirection.x);
            }

            // Set moving flag in Animator
            animator.SetBool("IsMoving", true);
        }
    }

    // Handle idle animations based on last horizontal movement direction
    void SetIdleAnimation()
    {
        if (rb.velocity == Vector2.zero && animator != null)
        {
            // Stop moving, so IsMoving should be false
            animator.SetBool("IsMoving", false);

            // Set LastMoveX for idle animation based on the last horizontal direction
            animator.SetFloat("LastMoveX", lastMoveDirection.x);
        }
    }

    // Update the IsMoving parameter based on the player's velocity
    void UpdateIsMoving()
    {
        if (animator != null)
        {
            bool isMoving = rb.velocity.magnitude > 0.1f;  // Consider the player moving if velocity is greater than a threshold
            animator.SetBool("IsMoving", isMoving);
        }
    }
}
