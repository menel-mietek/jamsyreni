using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class SceneReloader : MonoBehaviour
{
    // This method reloads the current active scene
    public void ReloadScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the scene by its name
        SceneManager.LoadScene(currentScene.name);
    }

    // Optional: Reload when a certain key is pressed, e.g., 'R' key
    void Update()
    {
        // If the 'R' key is pressed, reload the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }
}
