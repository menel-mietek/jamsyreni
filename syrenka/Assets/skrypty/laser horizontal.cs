using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserhorizontal : MonoBehaviour
{
    public float rayDistance = 10f;             // Max distance the laser can shoot
    public LayerMask collisionLayers;           // Layers for obstacles, including the player
    public LineRenderer laserRenderer;           // LineRenderer for the laser beam
    public Transform laserOrigin;                // Origin point for the laser
    public Color laserColor = Color.red;        // Color of the laser
    public float laserWidth = 0.1f;             // Width of the laser beam
    public Vector2 laserDirection = Vector2.down; // Direction of the laser (customizable in Inspector)

    void Start()
    {
        // Set up the LineRenderer properties for the laser
        SetupLineRenderer(laserRenderer);
    }

    void Update()
    {
        ShootLaser(laserOrigin, laserRenderer);
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
        // Cast a ray from the laser's origin position in the given direction
        RaycastHit2D hit = Physics2D.Raycast(origin.position, laserDirection.normalized, rayDistance, collisionLayers);

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
            lineRenderer.SetPosition(1, (Vector2)origin.position + laserDirection.normalized * rayDistance);
        }
    }
}
