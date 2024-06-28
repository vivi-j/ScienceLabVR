using Oculus.Interaction;
using UnityEngine;
using TMPro;


public class TestTubeManager : MonoBehaviour
{
    private bool isHeld = false; // Track if the test tube is being held
    private Transform originalParent; // To reset the parent if needed
    public Transform testTube1;
    public TextMeshProUGUI debugText;
    private Grabbable grabbable;

    void Start()
    {
        originalParent = transform.parent; // Store the original parent

        // Get the Grabbable component from the test tube
        grabbable = testTube1.GetComponent<Grabbable>();

        // Subscribe to grab and release events
        grabbable.WhenPointerEventRaised += HandlePointerEvent;
    }

    void Update()
    {
        // Print distance between test tube 1 and slot 1

        // Check if the test tube is being held
        if (!isHeld)
        {
            // Move the test tube back to the slot
            MoveTestTubeToSlot();
        }
    }

    private void HandlePointerEvent(PointerEvent evt)
    {
        if (evt.Type == PointerEventType.Select)
        {
            isHeld = true; // Test tube is being held
        }
        else if (evt.Type == PointerEventType.Unselect)
        {
            isHeld = false; // Test tube is released
        }
    }

    // Method to move the test tube back to the slot
    private void MoveTestTubeToSlot()
    {
        testTube1.position = originalParent.position;
        testTube1.rotation = originalParent.rotation;
    }
}
