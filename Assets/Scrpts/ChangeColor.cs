using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material newHandMaterial; // Assign this in the Inspector
    public GameObject gloves; // Assign the gloves GameObject in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Make the gloves disappear
        gloves.SetActive(false);
        Renderer handRenderer = other.GetComponent<Renderer>();
        if (handRenderer != null)
        {
            handRenderer.material = newHandMaterial;
        }

    }

}