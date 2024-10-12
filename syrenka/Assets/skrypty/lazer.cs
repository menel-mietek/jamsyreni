using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    public float rayDistance = 10f;             // Max distance the laser can shoot
    public LayerMask collisionLayers;           // Layers for obstacles, including the player
    public LineRenderer lineRenderer;           // LineRenderer for the laser beam
    public Color laserColor = Color.red;        // Color of the laser
    public float laserWidth = 0.1f;             // Width of the laser beam

    void Start()
    {
        // Set up the LineRenderer properties
        lineRenderer.startWidth = laserWidth;
        lineRenderer.endWidth = laserWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        // Define the ray direction (forward, in this case)
        Vector2 rayDirection = transform.up; // Shoots the laser to the right (can be changed)

        // Cast a ray from the object's position in the given direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance, collisionLayers);

        // Set the start point of the laser (the empty GameObject's position)
        lineRenderer.SetPosition(0, transform.position);

        if (hit.collider != null)
        {
            // Laser hits something
            lineRenderer.SetPosition(1, hit.point); // Stop the laser at the hit point

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
           
            lineRenderer.SetPosition(1, (Vector2)transform.position + rayDirection * rayDistance);
        }
    }
}
