using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using OculusSampleFramework;

public class Movement : MonoBehaviour
{
    public Transform character;  // Reference to the character's transform
    public Transform headset;  // Reference to the headset's transform
    public OVRSkeleton handSkeleton;  // Reference to the hand skeleton
    public float moveSpeed = 1.0f;  // Speed at which the character moves
    public float rotationSpeed = 30.0f;  // Speed at which the character rotates
    public bool movePlayer = false;
    public bool rotateLeft = false;
    public bool rotateRight = false;

    public TextMeshProUGUI debugText;

    string currentTime = "";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime = DateTime.Now.ToString("hh:mm:ss tt");

        // Get the index finger tip transform
        Transform indexFingerTip = handSkeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform;
        Vector3 moveDirection = indexFingerTip.forward;

        // Zero out the y-component to keep movement horizontal
        moveDirection.y = 0;
        moveDirection.Normalize();  // Normalize the direction vector

        // Get the headset's forward direction
        Vector3 headsetForward = headset.forward;
        headsetForward.y = 0;  // Zero out the y-component to keep rotation horizontal
        headsetForward.Normalize();  // Normalize the direction vector

        if (movePlayer)
        {
            // Move the character in the direction the index finger points, ignoring the y direction
            character.position += moveDirection * moveSpeed * Time.deltaTime;        // Rotate the character towards the direction the headset is facing
            //Quaternion targetRotation = Quaternion.LookRotation(headsetForward);
            //character.rotation = Quaternion.RotateTowards(character.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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
        debugText.text = currentTime + ": Moving";
    }

    public void StopMovement()
    {
        movePlayer = false;
        debugText.text = currentTime + ": Not Moving";
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
