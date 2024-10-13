using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isRedKey, isBlueKey, isGreenKey;

    // Reference to the AudioSource component
    public AudioSource audioSource;

    // Audio clip for the pickup sound
    public AudioClip pickupSound;

    private void Start()
    {
        // Ensure there is an AudioSource component attached
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Use OnTriggerEnter2D for 2D collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that collided has the Player tag
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

            // Play the pickup sound
            if (pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound); // Play the assigned sound
            }

            // Destroy the key object after picking it up
            Destroy(gameObject, pickupSound.length); // Destroy after sound finishes
        }
    }
}
