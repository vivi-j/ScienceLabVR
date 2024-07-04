using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RampActions : MonoBehaviour
{
    private Transform rampTransform;
    private MeshFilter meshFilter;

    public Renderer rampRenderer;

    public Material iceMaterial;
    public Material metalMaterial;
    public Material glassMaterial;
    public Material woodMaterial;

    private Vector3 initialScale;
    private Material initialMaterial;

    public Slider heightSlider;
    public TMP_Dropdown materialDropdown;
    private float initialHeightSliderValue;
    private int initialMaterialDropdownValue;

    public float staticFrictionCoefficient; // Static friction value
    public float rollingResistanceCoefficient; // Rolling resistance value

    public Transform sphereTransform; // Reference to the sphere
    private Rigidbody sphereRigidbody;

    private float rampLength = 0.5f; // Adjust this based on your ramp's length

    // TMP Text components for displaying values
    public TMP_Text velocityText;
    public TMP_Text accelerationText;
    public TMP_Text energyText;
    public TMP_Text rampHeight;


    void Start()
    {
        rampTransform = transform;
        initialScale = rampTransform.localScale;
        initialMaterial = rampRenderer.material;
        initialHeightSliderValue = heightSlider.value;
        initialMaterialDropdownValue = materialDropdown.value;
        staticFrictionCoefficient = 0.05f;
        rollingResistanceCoefficient = 0.02f;

        sphereRigidbody = sphereTransform.GetComponent<Rigidbody>();

        // Initialize TMP text fields if not assigned in the inspector
        if (velocityText == null)
            velocityText = GameObject.Find("VelocityText").GetComponent<TMP_Text>(); // Adjust "VelocityText" to your TMP Text object name
        if (accelerationText == null)
            accelerationText = GameObject.Find("AccelerationText").GetComponent<TMP_Text>(); // Adjust "AccelerationText" to your TMP Text object name
        if (energyText == null)
            energyText = GameObject.Find("EnergyText").GetComponent<TMP_Text>(); // Adjust "EnergyText" to your TMP Text object name
    }

    void Update()
    {
        CalculatePhysics();
    }

    public void UpdateHeight(float value)
    {
        Vector3 currentScale = rampTransform.localScale;
        rampTransform.localScale = new Vector3(currentScale.x, value, currentScale.z); 
        rampHeight.text = $"{value:F2} m";

    }

    public void ChangeMaterial(int index)
    {
        switch (index)
        {
            case 0: // ice on plastic
                rampRenderer.material = iceMaterial;
                staticFrictionCoefficient = 0.05f; // Example coefficient for ice
                rollingResistanceCoefficient = 0.02f; // Example rolling resistance for ice
                break;
            case 1: // metal on plastic
                rampRenderer.material = metalMaterial;
                staticFrictionCoefficient = 0.35f; // Example coefficient for metal
                rollingResistanceCoefficient = 0.3f; // Example rolling resistance for metal
                break;
            case 2: // glass on plastic
                rampRenderer.material = glassMaterial;
                staticFrictionCoefficient = 0.7f; // Example coefficient for glass
                rollingResistanceCoefficient = 0.5f; // Example rolling resistance for glass
                break;
            case 3: // wood on plastic
                rampRenderer.material = woodMaterial;
                staticFrictionCoefficient = 0.45f; // Example coefficient for wood
                rollingResistanceCoefficient = 0.4f; // Example rolling resistance for wood
                break;
        }
    }

    private void CalculatePhysics()
    {
        float height = rampTransform.localScale.y;
        float angle = Mathf.Atan(height / rampLength);
        float g = 9.81f;

        // Calculate acceleration
        float netForce = g * Mathf.Sin(angle) - staticFrictionCoefficient * g * Mathf.Cos(angle);
        float acceleration = (5f / 7f) * netForce;

        // Calculate final velocity considering rolling resistance
        float finalVelocity = Mathf.Sqrt(2 * acceleration * rampLength * (1 - rollingResistanceCoefficient));

        // Calculate kinetic energy
        float mass = sphereRigidbody.mass;
        float kineticEnergy = (7f / 10f) * mass * finalVelocity * finalVelocity;

        // Display values on TMP Text components rounded to 2 decimal places
        velocityText.text = $"{finalVelocity:F2} m/s";
        accelerationText.text = $"{acceleration:F2} m/s²";
        energyText.text = $"{kineticEnergy:F2} J";
    }

    public void ResetRamp()
    {
        rampTransform.localScale = initialScale;
        rampRenderer.material = initialMaterial;
        heightSlider.value = initialHeightSliderValue;
        materialDropdown.value = initialMaterialDropdownValue;
        staticFrictionCoefficient = 0.05f;
        rollingResistanceCoefficient = 0.02f;
    }
}
