using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour
{
    public LayerMask whatStopsMovement;  // Layer for obstacles
    public float tileSize = 1f;          // Size of each tile (1x1 units)

    // Method to try pushing the object in a specific direction
    public bool TryToPush(Vector2 direction)
    {
        // Calculate the new position where the object will be moved
        Vector2 newPosition = (Vector2)transform.position + direction * tileSize;

        // Check if the new position is free (no obstacles blocking)
        if (!Physics2D.OverlapCircle(newPosition, 0.1f, whatStopsMovement))
        {
            // Snap the object's position to the new tile
            transform.position = new Vector3(Mathf.Round(newPosition.x), Mathf.Round(newPosition.y), 0f);
            return true;  // Return true if the object was successfully pushed
        }

        return false;  // Return false if the object couldn't be pushed
    }
}