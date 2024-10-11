using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isRedKey, isBlueKey, isGreenKey;

    // Use OnTriggerEnter2D for 2D collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided has the Player tag
        Debug.Log("chuj");
        if (other.CompareTag("Player"))
        {
            // Access the player's inventory component
            Inventory playerInventory = other.GetComponent<Inventory>();

            // Check which key this pickup is and assign it to the inventory
            if (isRedKey)
            {
                playerInventory.hasRed = true;
                Debug.Log("Picked up Red Key");
            }

            if (isBlueKey)
            {
                playerInventory.hasBlue = true;
                Debug.Log("Picked up Blue Key");
            }

            if (isGreenKey)
            {
                playerInventory.hasGreen = true;
                Debug.Log("Picked up Green Key");
            }

            // Destroy the key object after picking it up
            Destroy(gameObject);
        }
    }
}
