using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementV3 : MonoBehaviour
{
    public Animator animator;  // Reference to the animator

    void Update()
    {
        HandleMovementInput();
    }

    // Function to handle key inputs for movement and updating the animator
    void HandleMovementInput()
    {
        // Walking left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animator.SetBool("IsWalkingLeft", true);
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetBool("IsWalkingLeft", false);
        }

        // Walking right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animator.SetBool("IsWalkingRight", true);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingUp", false);
            animator.SetBool("IsWalkingDown", false);
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("IsWalkingRight", false);
        }

        // Walking up
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("IsWalkingUp", true);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsWalkingDown", false);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("IsWalkingUp", false);
        }

        // Walking down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("IsWalkingDown", true);
            animator.SetBool("IsWalkingLeft", false);
            animator.SetBool("IsWalkingRight", false);
            animator.SetBool("IsWalkingUp", false);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("IsWalkingDown", false);
        }
    }
}
