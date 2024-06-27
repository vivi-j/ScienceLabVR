using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LiquidPour : MonoBehaviour
{
    //public ParticleSystem pourParticleSystem; // Reference to the particle system
    public float fillAmount = 0.2f; // Initial fill amount (1.0 = full, 0.0 = empty)
    public float pourRate = 0.001f; // Rate at which liquid decreases per second when pouring
    public TextMeshProUGUI debugText;
    public Material liquidMaterial; // access material
    public GameObject streamPrefab;
    public Transform origin = null;
    //private Stream currentStream = null;
    private bool isPouring = false;
    public ParticleSystem pourParticleSystem;

    void Awake()
    {
        pourParticleSystem.Stop();
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
        if (Mathf.Abs(xAngle) > 90 || Mathf.Abs(zAngle) > 90)
        {
            isPouring = true;
        }
        else
        {
            isPouring = false;
        }


        // If pouring, decrease the fill amount
        if (isPouring && fillAmount > 0.005)
        {
            pourParticleSystem.Play();
            //currentStream = CreateStream();
            //currentStream.Begin();
            fillAmount -= pourRate * Time.deltaTime;
            fillAmount = Mathf.Clamp(fillAmount, 0.0f, 0.2f);
            debugText.text = "TRYING TO POUR, FILL AMOUNT IS: " + fillAmount;
            liquidMaterial.SetFloat("_Fill", fillAmount);
        }
        else
        {
            if(fillAmount <= 0.005)
            {
                pourParticleSystem.Stop();
            }

        }
    }

    private Stream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<Stream>();
    }
}
