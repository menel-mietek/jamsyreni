using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosslaser : MonoBehaviour
{
    public float rayDistance = 10f;             // Max distance the laser can shoot
    public LayerMask collisionLayers;           // Layers for obstacles, including the player
    public LineRenderer lineRenderer;           // LineRenderer for the laser beam
    public LineRenderer indicatorRenderer;      // LineRenderer for the laser indicator
    public Color laserColor = Color.red;        // Color of the laser
    public Color indicatorColor = Color.green;  // Color of the indicator
    public float laserWidth = 0.1f;             // Width of the laser beam
    public float indicatorWidth = 0.05f;        // Width of the indicator beam
    public Vector2 laserDirection = Vector2.up; // Direction of the laser (customizable in Inspector)

    // Timing variables
    public float indicatorVisibleDuration = 4f; // How long the indicator is visible
    public float laserVisibleDuration = 2f;     // How long the laser is visible
    public float cooldownDuration = 2f;         // Cooldown between laser cycles

    private bool isLaserActive = false;         // Tracks if the laser is active

    void Start()
    {
        // Set up the LineRenderer properties for the laser
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;

        // Set up the LineRenderer properties for the indicator
        indicatorRenderer.startWidth = indicatorWidth;
        indicatorRenderer.endWidth = indicatorWidth;
        indicatorRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        indicatorRenderer.startColor = indicatorColor;
        indicatorRenderer.endColor = indicatorColor;

        // Start the coroutine to handle the indicator and laser behavior
        StartCoroutine(IndicatorLaserRoutine());
    }

    // Coroutine to control the indicator and laser sequence
    IEnumerator IndicatorLaserRoutine()
    {
        while (true)
        {
            // Show the indicator for 4 seconds
            indicatorRenderer.enabled = true;
            UpdateLaserIndicator();  // Update the indicator's position and direction
            yield return new WaitForSeconds(indicatorVisibleDuration);

            // Hide the indicator
            indicatorRenderer.enabled = false;

            // Fire the laser for 2 seconds
            isLaserActive = true;
            ShootLaser();
            yield return new WaitForSeconds(laserVisibleDuration);

            // Turn off the laser after firing
            lineRenderer.enabled = false;
            isLaserActive = false;

            // Cooldown of 2 seconds before the next cycle
            yield return new WaitForSeconds(cooldownDuration);
        }
    }

    // Method to shoot the laser (only if the laser is active)
    void ShootLaser()
    {
        if (isLaserActive)
        {
            // Cast a ray from the object's position in the given direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection.normalized, rayDistance, collisionLayers);

            // Set the start point of the laser
            lineRenderer.enabled = true;  // Enable the laser line
            lineRenderer.SetPosition(0, transform.position);

            if (hit.collider != null)
            {
                // Laser hits something
                lineRenderer.SetPosition(1, hit.point); // Stop the laser at the hit point

                // Check if the hit object has the "Player" tag
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("Player hit by laser! Player dies.");
                    Destroy(hit.collider.gameObject); // Destroy the player object
                }
                else
                {
                    Debug.Log("Laser hit: " + hit.collider.name);
                }
            }
            else
            {
                // No hit, laser goes to max range
                lineRenderer.SetPosition(1, (Vector2)transform.position + laserDirection.normalized * rayDistance);
            }
        }
    }

    // Method to update the laser indicator
    void UpdateLaserIndicator()
    {
        // Cast a ray from the object's position to visualize the indicator
        RaycastHit2D hit = Physics2D.Raycast(transform.position, laserDirection.normalized, rayDistance, collisionLayers);

        // Set the start point of the indicator
        indicatorRenderer.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            // The indicator hits something, so we stop the indicator at that point
            indicatorRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // No hit, indicator goes to the max distance
            indicatorRenderer.SetPosition(1, (Vector2)transform.position + laserDirection.normalized * rayDistance);
        }
    }
}
