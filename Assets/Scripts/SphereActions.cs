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


    public void UpdateRadius(float value)
    {
        float newRadius = Mathf.Lerp(0.1f, 0.4f, value);
        sphereTransform.localScale = new Vector3(newRadius, newRadius, newRadius);
        //NotifyRampActions();
        ballRadius.text = $"{newRadius:F2} m";
    }

    public void UpdateMass(float value)
    {
        float newMass = Mathf.Lerp(1.0f, 1000.0f, value);
        sphereRigidbody.mass = newMass;
        //NotifyRampActions();
        ballMass.text = $"{value:F2} kg";
    }

    /*  private void NotifyRampActions()
      {
          RampActions rampActions = FindObjectOfType<RampActions>();
          if (rampActions != null)
          {
              rampActions.CalculatePhysics();
          }
      }*/

    public void ResetSphere()
    {
        sphereTransform.position = initialPosition;
        sphereTransform.rotation = initialRotation;
        sphereTransform.localScale = initialScale;
        sphereRigidbody.mass = initialMass;

        radiusSlider.value = initialRadiusSliderValue;
        massSlider.value = initialMassSliderValue;

       // NotifyRampActions();
    }

    public void Respawn()
    {
        sphereTransform.position = initialPosition;
    }
}
