using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterActivation : MonoBehaviour
{
    public ParticleSystem waterParticleSystem;
    public AudioSource waterAudioSource;


    void Awake()
    {
        // disable particle system
        waterParticleSystem.Stop();
        waterAudioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // turn on particle system
        waterParticleSystem.Play();
        waterAudioSource.Play();
    }

    // on trigger exit, stop the particle system
    private void OnTriggerExit(Collider other)
    {
        // Disable the particle system when an object exits the trigger
        waterParticleSystem.Stop();
        waterAudioSource.Stop();
    }

}
