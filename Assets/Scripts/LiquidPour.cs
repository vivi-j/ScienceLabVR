using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LiquidPour : MonoBehaviour
{
    public float fillAmount = 0.13f; // Initial fill amount (1.0 = full, 0.0 = empty)
    public float pourRate = 0.01f; // Rate at which liquid decreases per second when pouring
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI particleSystemStateText; // New TMP component for particle system state
    public Material liquidMaterial; // Access material
    private bool isPouring = false;
    public ParticleSystem pourParticleSystem;
    public AudioSource pourAudioSource; // Reference to the audio source

    void Awake()
    {
        // Ensure the particle system is stopped initially
        pourParticleSystem.Stop();
        // Get the AudioSource component attached to the same GameObject
        pourAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Get the rotation angles of the glass
        float xAngle = transform.rotation.eulerAngles.x;
        float zAngle = transform.rotation.eulerAngles.z;

        // Adjust the angles to handle the 360 to 0 degree transition
        if (xAngle > 180) xAngle -= 360;
        if (zAngle > 180) zAngle -= 360;

        // Check if the glass is tilted enough to pour
        if (Mathf.Abs(xAngle) > 60 || Mathf.Abs(zAngle) > 60)
        {
            isPouring = true;
        }
        else
        {
            isPouring = false;
        }

        // If pouring, decrease the fill amount
        if (isPouring && fillAmount > 0.0f)
        {
            fillAmount -= pourRate * Time.deltaTime;
            fillAmount = Mathf.Clamp(fillAmount, 0.0f, 0.2f);
            debugText.text = "TRYING TO POUR, FILL AMOUNT IS: " + fillAmount;
            liquidMaterial.SetFloat("_Fill", fillAmount);
        }
        else
        {
            debugText.text = "STOPPING POUR, FILL AMOUNT IS: " + fillAmount;
        }

        // Handle particle system and audio based on debugText
        if (debugText.text.Contains("TRYING TO POUR"))
        {
                pourParticleSystem.Play(); // Start the particle system
                particleSystemStateText.text = "Particle System: Active";
            
            if (!pourAudioSource.isPlaying)
            {
                pourAudioSource.Play(); // Start the audio if not already playing
            }
        }
        else
        {
            if (pourParticleSystem.isPlaying)
            {
                pourParticleSystem.Stop(); // Stop the particle system if it is playing
                particleSystemStateText.text = "Particle System: Inactive";
            }
            if (pourAudioSource.isPlaying)
            {
                pourAudioSource.Stop(); // Stop the audio if it is playing
            }
        }
    }
}
