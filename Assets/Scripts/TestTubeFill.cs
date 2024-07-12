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
    private List<string> collidedObjects; // List to keep track of collided objects

    void Start()
    {
        currentColor = new Color(1, 1, 1, 0); // Transparent color
        targetLiquidColor = currentColor; // Initialize target color to transparent
        testTubeMaterial.SetColor("_Liquid", currentColor);
        collidedObjects = new List<string>(); // Initialize the list
    }

    void Update()
    {
        // Smoothly transition the liquid color towards the target color
        if (testTubeMaterial != null)
        {
            currentColor = testTubeMaterial.GetColor("_Liquid");
            testTubeMaterial.SetColor("_Liquid", Color.Lerp(currentColor, targetLiquidColor, Time.deltaTime));
        }

        // Check if both "POTASSIUM" and "IODINE" are in the list
        if (collidedObjects.Contains("POTASSIUM-PERMANGANATE") && collidedObjects.Contains("POTASSIUM-DICHROMATE"))
        {
            debugText.text = "Mixing Result: Potassium Permanganate and Potassium Dichromate mixed to create Chromium(III) Sulfate solution!";
        }
        if (collidedObjects.Contains("POTASSIUM-PERMANGANATE") && collidedObjects.Contains("FERRIC-CHLORIDE"))
        {
            debugText.text = "Mixing Result: Potassium Permanganate and Ferric Chloride mixed to create Iron(III) Chloride solution!";
        }
        if (collidedObjects.Contains("POTASSIUM-PERMANGANATE") && collidedObjects.Contains("NICKEL-SULFATE"))
        {
            debugText.text = "Mixing Result: Potassium Permanganate and Nickel Sulfate mixed to create Nickel(III) Sulfate solution!";
        }
        if (collidedObjects.Contains("FERRIC-CHLORIDE") && collidedObjects.Contains("POTASSIUM-DICHROMATE"))
        {
            debugText.text = "Mixing Result: Potassium Dichromate and Ferric Chloride mixed to create Chromium(III) Chloride solution!";
        }
        if (collidedObjects.Contains("FERRIC-CHLORIDE") && collidedObjects.Contains("NICKEL-SULFATE"))
        {
            debugText.text = "Mixing Result: Ferric Chloride and Nickel Sulfate mixed to create Nickel(II) chloride solution!";
        }
        if (collidedObjects.Contains("POTASSIUM-DICHROMATE") && collidedObjects.Contains("NICKEL-SULFATE"))
        {
            debugText.text = "Mixing Result: Potassium Dichromate and Nickel Sulfate mixed to create a Nickel(II) Dichromate solution!";
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string collidedObjectName = collision.gameObject.name.ToUpper();

        // Check if the collided object is either "POTASSIUM" or "IODINE"
        if (collidedObjectName == "POTASSIUM-PERMANGANATE" || collidedObjectName == "POTASSIUM-DICHROMATE" || collidedObjectName == "NICKEL-SULFATE" || collidedObjectName == "FERRIC-CHLORIDE")
        {
            // Add the object to the list if it hasn't been added already
            if (!collidedObjects.Contains(collidedObjectName))
            {
                collidedObjects.Add(collidedObjectName);
                //debugText.text += "Collided with: " + collidedObjectName + "\n";
            }

            // Process the collision
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
        string collidedObjectName = other.name.ToUpper();

        // Check if the collided object is either "POTASSIUM" or "IODINE"
        if (collidedObjectName == "POTASSIUM-PERMANGANATE" || collidedObjectName == "POTASSIUM-DICHROMATE" || collidedObjectName == "NICKEL-SULFATE" || collidedObjectName == "FERRIC-CHLORIDE")
        {
            // Add the object to the list if it hasn't been added already
            if (!collidedObjects.Contains(collidedObjectName))
            {
                collidedObjects.Add(collidedObjectName);
                debugText.text += "Particle collision with: " + collidedObjectName + "\n";
            }

            // Process the collision
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
}