using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCollider : MonoBehaviour
{
    // You can specify which key is required to access the win collider
    public bool requiresRedKey = true;
    public bool requiresBlueKey = false;
    public bool requiresGreenKey = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the Inventory component from the player
            Inventory playerInventory = other.GetComponent<Inventory>();

            // Check if the player has the required keys
            if (playerInventory != null)
            {
                bool canAccessWinArea = true;

                if (requiresRedKey && !playerInventory.hasRed)
                {
                    canAccessWinArea = false;
                }

                if (requiresBlueKey && !playerInventory.hasBlue)
                {
                    canAccessWinArea = false;
                }

                if (requiresGreenKey && !playerInventory.hasGreen)
                {
                    canAccessWinArea = false;
                }

                // If the player has the required keys, allow access
                if (canAccessWinArea)
                {
                    Debug.Log("Player reached the win area!");
                    // Load the win screen or any other action you want to take
                    SceneManager.LoadScene("Win", LoadSceneMode.Single);
                }
                else
                {
                    Debug.Log("You need the correct key(s) to enter the win area!");
                }
            }
        }
    }
}
