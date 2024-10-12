using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public float rayDistance = 10f;             // Max distance the lasers can shoot
    public LayerMask collisionLayers;           // Layers for obstacles, including the player
    public LineRenderer laser1Renderer;         // LineRenderer for the first laser beam
    public LineRenderer laser2Renderer;         // LineRenderer for the second laser beam
    public Transform laser1Origin;              // Origin point for the first laser
    public Transform laser2Origin;              // Origin point for the second laser
    public Color laserColor = Color.red;        // Color of the lasers
    public float laserWidth = 0.1f;             // Width of the laser beams

    void Start()
    {
        // Set up the LineRenderer properties for both lasers
        SetupLineRenderer(laser1Renderer);
        SetupLineRenderer(laser2Renderer);
    }

    void Update()
    {
        ShootLaser(laser1Origin, laser1Renderer);
        ShootLaser(laser2Origin, laser2Renderer);
    }

    // Helper function to set up a line renderer
    void SetupLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    // Function to shoot a laser from a given origin and display it using the given LineRenderer
    void ShootLaser(Transform origin, LineRenderer lineRenderer)
    {
        // Define the ray direction (upwards, change if needed for different directions)
        Vector2 rayDirection = origin.up;

        // Cast a ray from the laser's origin position in the given direction
        RaycastHit2D hit = Physics2D.Raycast(origin.position, rayDirection, rayDistance, collisionLayers);

        // Set the start point of the laser (the laser's origin position)
        lineRenderer.SetPosition(0, origin.position);

        if (hit.collider != null)
        {
            // Laser hits something, stop at the hit point
            lineRenderer.SetPosition(1, hit.point);

            // Check if the hit object has the "Player" tag
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player hit by laser! Player dies.");
                Destroy(hit.collider.gameObject); // Destroy the player object (or trigger death)
            }
            else
            {
                Debug.Log("Laser hit: " + hit.collider.name);
            }
        }
        else
        {
            // No collision, extend the laser to its max distance
            lineRenderer.SetPosition(1, (Vector2)origin.position + rayDirection * rayDistance);
        }
    }
}
