using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChemistry : MonoBehaviour
{
    private GameObject chemistryObject; // Reference to the Chemistry GameObject
    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();
    private Dictionary<Transform, Vector3> originalScales = new Dictionary<Transform, Vector3>();

    // Public references to materials
    public Material[] publicMaterials; // Array to hold the 4 public materials
    public Material[] testTubeMaterials; // Array to hold the 3 test tube materials


    void Awake()
    {
        chemistryObject = GameObject.Find("CHEMISTRY"); // Replace with actual name if different

        // Record initial positions, rotations, and scales of all children
        foreach (Transform child in chemistryObject.transform)
        {
            originalPositions[child] = child.localPosition;
            originalRotations[child] = child.localRotation;
            originalScales[child] = child.localScale;
        }
    }

    public void ResetChildren()
    {
        foreach (Transform child in chemistryObject.transform)
        {
            // Reset position, rotation, and scale to original values
            child.localPosition = originalPositions[child];
            child.localRotation = originalRotations[child];
            child.localScale = originalScales[child];


            // Reset other components or scripts attached to the child if needed
            // Example: ResetComponent(child.GetComponent<MyComponent>());

            // Reset any other properties or values specific to your GameObjects
            // Example: child.GetComponent<MyCustomScript>().ResetToDefaults();
            // Set _Fill values for public materials
            foreach (Material mat in publicMaterials)
            {
                mat.SetFloat("_Fill", 2.0f);
            }

            // Set _Fill values for test tube materials
            foreach (Material mat in testTubeMaterials)
            {
                mat.SetFloat("_Fill", 0.0f);
            }

        }
    }


   
}
