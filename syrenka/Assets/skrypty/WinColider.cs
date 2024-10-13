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

                // If the player has the required keys, allow access and load the next scene
                if (canAccessWinArea)
                {
                    Debug.Log("Player reached the win area!");

                    // Get the current scene index
                    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                    // Load the next scene if available
                    if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
                    }
                    else
                    {
                        Debug.Log("This is the last scene. No next scene to load.");
                        // Optionally, handle what to do if there's no next scene
                    }
                }
                else
                {
                    Debug.Log("You need the correct key(s) to enter the win area!");
                }
            }
        }
    }
}

