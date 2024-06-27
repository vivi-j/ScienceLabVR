using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterActivation : MonoBehaviour
{
    public ParticleSystem waterParticleSystem;
    public AudioSource waterAudioSource;
    public TextMeshProUGUI debugText;

    //ParticleSystem.CollisionModule collisionModule;

    void Awake()
    {
        // Disable particle system and audio source initially
        waterParticleSystem.Stop();
        waterAudioSource.Stop();

        // Get Collision module
        //collisionModule = waterParticleSystem.collision;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Set debug text to indicate collision
        debugText.text = "Collided with: " + other.gameObject.name;
        /*
        // Enable collision module and adjust settings
        collisionModule.enabled = true;
        collisionModule.radiusScale = 1.0f; // Adjust as needed
        collisionModule.dampen = 0.5f;      // Adjust as needed
        collisionModule.maxKillSpeed = 10.0f; // Adjust as needed
        */
        // Play particle system and audio source
        waterParticleSystem.Play();
        waterAudioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        // Clear debug text
        debugText.text = "";

        // Disable collision module
        //collisionModule.enabled = false;

        // Stop particle system and audio source
        waterParticleSystem.Stop();
        waterAudioSource.Stop();
    }
}
