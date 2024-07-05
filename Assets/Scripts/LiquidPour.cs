using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LiquidPour : MonoBehaviour
{
    //public ParticleSystem pourParticleSystem; // Reference to the particle system
    public float fillAmount = 0.13f; // Initial fill amount (1.0 = full, 0.0 = empty)
    public float pourRate = 0.01f; // Rate at which liquid decreases per second when pouring
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI debugText1;
    public Material liquidMaterial; // access material
    private bool isPouring = false;
    public ParticleSystem pourParticleSystem;
    //public GameObject stream;

    void Awake()
    {
        pourParticleSystem.Stop();
        //stream.SetActive(false);
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
        if (isPouring && (fillAmount > 0.005))
        {
            pourParticleSystem.Play();
            fillAmount -= pourRate * Time.deltaTime;
            fillAmount = Mathf.Clamp(fillAmount, 0.0f, 0.2f);
            debugText.text = "TRYING TO POUR, FILL AMOUNT IS: " + fillAmount;
            liquidMaterial.SetFloat("_Fill", fillAmount);
            //stream.SetActive(true);
        }
        else
        {
            debugText.text = "STOPPING POUR, FILL AMOUNT IS: " + fillAmount;
            pourParticleSystem.Stop();
            //stream.SetActive(false);
        }
    }
}
