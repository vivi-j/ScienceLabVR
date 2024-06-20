using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform character;  // Reference to the character's transform
    public float moveSpeed = 1.0f;  // Speed at which the character moves
    public float rotationSpeed = 90.0f;  // Speed at which the character rotates
    public bool movePlayer = false;
    public bool rotateLeft = false;
    public bool rotateRight = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movePlayer)
        {
            Vector3 forwardDirection = character.forward;  // Get the forward direction of the character
            character.position += forwardDirection * moveSpeed * Time.deltaTime;  // Move the character forward
        }

        if (rotateLeft)
        {
            character.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);  // Rotate the character to the left
        }

        if (rotateRight)
        {
            character.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);  // Rotate the character to the right
        }
    }

    public void Move()
    {
        movePlayer = true;
    }

    public void StopMovement()
    {
        movePlayer = false;
    }

    public void StartRotateLeft()
    {
        rotateLeft = true;
    }

    public void StopRotateLeft()
    {
        rotateLeft = false;
    }

    public void StartRotateRight()
    {
        rotateRight = true;
    }

    public void StopRotateRight()
    {
        rotateRight = false;
    }
}
