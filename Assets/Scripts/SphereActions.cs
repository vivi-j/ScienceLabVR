using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SphereActions : MonoBehaviour
{
    private Transform sphereTransform;
    private Rigidbody sphereRigidbody;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private float initialMass;

    public Slider radiusSlider;
    public Slider massSlider;

    private float initialRadiusSliderValue;
    private float initialMassSliderValue;

    public TMP_Text ballMass;
    public TMP_Text ballRadius;

    public Button increaseRadiusButton;
    public Button decreaseRadiusButton;

    public Button increaseMassButton;
    public Button decreaseMassButton;

    private const float MinRadius = 0.1f;
    private const float MaxRadius = 0.4f;
    private const float RadiusStep = 0.05f;

    private const float MinMass = 1.0f;
    private const float MaxMass = 10.0f;
    private const float MassStep = 1.0f;


    void Start()
    {
        sphereTransform = transform;
        sphereRigidbody = GetComponent<Rigidbody>();

        // Store initial values
        initialPosition = sphereTransform.position;
        initialRotation = sphereTransform.rotation;
        initialScale = sphereTransform.localScale;
        initialMass = sphereRigidbody.mass;

        // Store initial slider and dropdown values
        initialRadiusSliderValue = radiusSlider.value;
        initialMassSliderValue = massSlider.value;
    }

    public void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the sphere collided with an object tagged "Spawn"
        if (collision.gameObject.CompareTag("Spawn"))
        {
            Respawn();
        }
    }



    public void UpdateRadius(float value)
    {
        sphereTransform.localScale = new Vector3(value, value, value);
        ballRadius.text = $"{value:F2} m";
        UpdateButtonStatesRadius(value);
        radiusSlider.value = value;
    }


    public void IncreaseRadius()
    {
        float currentRadius = sphereTransform.localScale.x;
        float newRadius = Mathf.Clamp(currentRadius + RadiusStep, MinRadius, MaxRadius);
        sphereTransform.localScale = new Vector3(newRadius, newRadius, newRadius);
        ballRadius.text = $"{newRadius:F2} m";
        radiusSlider.value = newRadius;
        UpdateButtonStatesRadius(newRadius);
    }

    public void DecreaseRadius()
    {
        float currentRadius = sphereTransform.localScale.x;
        float newRadius = Mathf.Clamp(currentRadius - RadiusStep, MinRadius, MaxRadius);
        sphereTransform.localScale = new Vector3(newRadius, newRadius, newRadius);
        ballRadius.text = $"{newRadius:F2} m";
        radiusSlider.value = newRadius;
        UpdateButtonStatesRadius(newRadius);
    }

    private void UpdateButtonStatesRadius(float currentRadius)
    {
        increaseRadiusButton.interactable = currentRadius < MaxRadius;
        decreaseRadiusButton.interactable = currentRadius > MinRadius;
    }


    public void UpdateMass(float value)
    {
        sphereRigidbody.mass = value;
        ballMass.text = $"{value:F2} kg";
        massSlider.value = value;
        UpdateButtonStatesMass(value);
    }

    public void IncreaseMass()
    {
        float currentMass = sphereRigidbody.mass;
        float newMass = Mathf.Clamp(currentMass + MassStep, MinMass, MaxMass);
        sphereRigidbody.mass = newMass;
        ballMass.text = $"{newMass:F2} kg";
        massSlider.value = newMass;
        UpdateButtonStatesMass(newMass);
    }

    public void DecreaseMass()
    {
        float currentMass = sphereRigidbody.mass;
        float newMass = Mathf.Clamp(currentMass - MassStep, MinMass, MaxMass);
        sphereRigidbody.mass = newMass;
        ballMass.text = $"{newMass:F2} kg";
        massSlider.value = newMass;
        UpdateButtonStatesMass(newMass);
    }

    private void UpdateButtonStatesMass(float currentMass)
    {
        increaseMassButton.interactable = currentMass < MaxMass;
        decreaseMassButton.interactable = currentMass > MinMass;
    }

    public void ResetSphere()
    {
        sphereTransform.position = initialPosition;
        sphereTransform.rotation = initialRotation;
        sphereTransform.localScale = initialScale;
        sphereRigidbody.mass = initialMass;
        radiusSlider.value = initialRadiusSliderValue;
        massSlider.value = initialMassSliderValue;
    }

    public void Respawn()
    {
        sphereTransform.position = initialPosition;
    }
}
