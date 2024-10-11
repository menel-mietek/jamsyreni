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

    void Start()
    {
        movepoint.parent = null;  // Ensure the movepoint is not parented to the player
        movepoint.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0f);  // Snap to grid
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, MoveSpeed * Time.deltaTime);

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
        }
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
        }
    }

    // Method to snap any position to the nearest tile
    Vector3 SnapToTile(Vector3 position)
    {
        return new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), 0f);
    }
}