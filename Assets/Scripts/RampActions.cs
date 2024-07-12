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

    public Button increaseHeightButton;
    public Button decreaseHeightButton;


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
        heightSlider.value = value;
        UpdateButtonStates(value);
    }

    public void IncreaseHeight()
    {
        Vector3 currentScale = rampTransform.localScale;
        float newYScale = Mathf.Clamp(currentScale.y + 0.1f, 0.1f, 1.0f);
        rampTransform.localScale = new Vector3(currentScale.x, newYScale, currentScale.z);
        rampHeight.text = $"{newYScale:F2} m";
        heightSlider.value = newYScale;
        UpdateButtonStates(newYScale);
    }

    public void DecreaseHeight()
    {
        Vector3 currentScale = rampTransform.localScale;
        float newYScale = Mathf.Clamp(currentScale.y - 0.1f, 0.1f, 1.0f);
        rampTransform.localScale = new Vector3(currentScale.x, newYScale, currentScale.z);
        rampHeight.text = $"{newYScale:F2} m";
        heightSlider.value = newYScale;
        UpdateButtonStates(newYScale);
    }

    private void UpdateButtonStates(float currentHeight)
    {
        increaseHeightButton.interactable = currentHeight < 1.0f;
        decreaseHeightButton.interactable = currentHeight > 0.1f;
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
        // Calculate height and angle of the ramp
        float height = rampTransform.localScale.y;
        float angle = Mathf.Atan(height / rampLength);
        float g = 9.81f; // gravitational constant

        // Moment of inertia (for a sphere, I = (2/5) * m * r^2)
        float radius = sphereTransform.localScale.x / 1f; // Assuming uniform scaling and sphere is scaled uniformly
        float momentOfInertia = (2f / 5f) * sphereRigidbody.mass * Mathf.Pow(radius, 2f);

        // Calculate net force including friction
        float normalForce = sphereRigidbody.mass * g * Mathf.Cos(angle);
        float frictionForce = staticFrictionCoefficient * normalForce;
        float netForce = sphereRigidbody.mass * g * Mathf.Sin(angle) - frictionForce;

        // Calculate acceleration using torque (τ = I * α)
        float torque = netForce * radius; // Torque = force * radius
        float angularAcceleration = torque / momentOfInertia;

        // Calculate linear acceleration considering rolling motion
        float linearAcceleration = angularAcceleration * radius; // α * r = a

        // Calculate final velocity considering rolling resistance
        Vector3 velocity = sphereRigidbody.velocity;
        float finalVelocity = Mathf.Abs(velocity.magnitude*(1-staticFrictionCoefficient)); // Take absolute value of magnitude

        // Calculate kinetic energy
        float kineticEnergy = (0.5f) * sphereRigidbody.mass * finalVelocity * finalVelocity;

        if(finalVelocity == 0.0f)
            accelerationText.text = $"{linearAcceleration * 0.0:F2} m/s²";
        else
            accelerationText.text = $"{linearAcceleration*0.3:F2} m/s²";
        

        // Display values on TMP Text components rounded to 2 decimal places
        velocityText.text = $"{finalVelocity:F2} m/s";
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
