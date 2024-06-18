using UnityEngine;

public class HandMaterialSwitcher : MonoBehaviour
{
    public SkinnedMeshRenderer handMeshRenderer; // Assign this in the Inspector
    public GameObject gloves; // Assign the Gloves GameObject in the Inspector
    public Material[] handMaterials; // Ensure this matches the materials array in the SkinnedMeshRenderer

    void Start()
    {
        if (gloves == null || handMeshRenderer == null || handMaterials.Length == 0)
        {
            Debug.LogError("Please assign all required references.");
            return;
        }

        // Use material at index 0 initially
        handMeshRenderer.material = handMaterials[0];

    }

    void Update()
    {
        // Check if Gloves object is not active
        if (!gloves.activeInHierarchy)
        {
            // Change the material to index 1
            handMeshRenderer.material = handMaterials[1];
        }

    }
}
