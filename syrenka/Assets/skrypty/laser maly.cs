using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasermaly : MonoBehaviour
{
    public float rayDistance = 10f;             // Max distance the laser can shoot
    public LayerMask collisionLayers;           // Layers for obstacles, including the player
    public LineRenderer lineRenderer;           // LineRenderer for the laser beam
    public Transform laserOrigin;               // Empty GameObject from where the raycast originates
    public Color laserColor = Color.red;        // Color of the laser
    public float laserWidth = 0.1f;             // Width of the laser beam
    public Vector2 laserDirection = Vector2.up; // Direction of the laser (customizable in Inspector)

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
        // Cast a ray from the empty GameObject's position in the specified direction
        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, laserDirection.normalized, rayDistance, collisionLayers);

        // Set the start point of the laser to the empty GameObject's position
        lineRenderer.SetPosition(0, laserOrigin.position);

        if (hit.collider != null)
        {
            // Laser hits something, stop the laser at the hit point
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
            lineRenderer.SetPosition(1, (Vector2)laserOrigin.position + laserDirection.normalized * rayDistance);
        }
    }
}
