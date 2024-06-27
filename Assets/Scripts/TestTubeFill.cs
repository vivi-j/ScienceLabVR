using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestTubeFill : MonoBehaviour
{
    public Material testTubeMaterial;
    public float fillIncreaseAmount = 0.005f;
    private float currentFillAmount = 0f;
    private Color targetLiquidColor;
    private Color currentColor;
    public TextMeshProUGUI debugText;

    public Transform rackTransform; // Reference to the rack GameObject
    public float snapDistance = 0.2f; // Distance within which the test tube will snap to a slot
    private Rigidbody tubeRigidbody;


    void Start()
    {
        currentColor = new Color(1, 1, 1, 0); // Transparent color
        targetLiquidColor = currentColor; // Initialize target color to transparent
        testTubeMaterial.SetColor("_Liquid", currentColor);
    }

    void Update()
    {
        // Smoothly transition the liquid color towards the target color
        if (testTubeMaterial != null)
        {
            currentColor = testTubeMaterial.GetColor("_Liquid");
            testTubeMaterial.SetColor("_Liquid", Color.Lerp(currentColor, targetLiquidColor, Time.deltaTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        debugText.text = "Collided with: " + collision.gameObject.name + "\n";


        if (collision.gameObject.CompareTag("Potassium"))
        {
            IncreaseFillAmount();
            // Get the color from the collided object's child's material
            Transform childTransform = collision.transform.GetChild(0); // Assuming the child is the first child
            if (childTransform != null)
            {
                Renderer childRenderer = childTransform.GetComponent<Renderer>();
                if (childRenderer != null)
                {
                    Material childMaterial = childRenderer.material;
                    if (childMaterial.HasProperty("_Liquid"))
                    {
                        Color newLiquidColor = childMaterial.GetColor("_Liquid");
                        MixLiquidColor(newLiquidColor); // Use mix instead of set
                    }
                }
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        debugText.text = "Particle collision with: " + other.name + "\n";

        IncreaseFillAmount();

        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            if (mainModule.startColor.mode == ParticleSystemGradientMode.Color)
            {
                MixLiquidColor(mainModule.startColor.color); // Use mix instead of set
            }
            else if (mainModule.startColor.mode == ParticleSystemGradientMode.Gradient)
            {
                // For gradients, we can pick a color from the gradient
                MixLiquidColor(mainModule.startColor.gradient.Evaluate(Random.value)); // Use mix instead of set
            }
        }
    }


    void IncreaseFillAmount()
    {
        currentFillAmount += fillIncreaseAmount;
        currentFillAmount = Mathf.Clamp01(currentFillAmount); // Clamp between 0 and 1

        if (testTubeMaterial != null)
        {
            testTubeMaterial.SetFloat("_Fill", currentFillAmount);
        }
    }

    void MixLiquidColor(Color newColor)
    {
        if (currentColor.r == 1f && currentColor.g == 1f && currentColor.b == 1f && currentColor.a == 0f)
        {
            targetLiquidColor = newColor; // Directly set to new color
        }
        else
        {
            targetLiquidColor = Color.Lerp(currentColor, newColor, 0.5f); // Adjust the blending factor as needed
        }
    }

  /*  void TrySnapToSlot()
    {
        Transform closestSlot = null;
        float closestDistance = float.MaxValue;

        // Iterate over all child slots of the rack
        foreach (Transform slot in rackTransform)
        {
            float distance = Vector3.Distance(transform.position, slot.position);
            if (distance < closestDistance && distance < snapDistance)
            {
                closestDistance = distance;
                closestSlot = slot;
            }
        }

        // Snap to the closest slot if within the snap distance
        if (closestSlot != null)
        {
            transform.position = closestSlot.position;
            transform.rotation = closestSlot.rotation;
            tubeRigidbody.isKinematic = true; // Disable physics to keep it in place
        }
    }*/



}
