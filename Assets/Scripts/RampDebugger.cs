using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RampDebugger : MonoBehaviour
{
    public TextMeshProUGUI debugtext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // Log the name of the object this ramp collided with
        debugtext.text = "Collided with: " + collision.gameObject.name;
    }

    void OnTriggerEnter(Collider other)
    {
        // Log the name of the object this ramp collided with if using triggers
        debugtext.text = "Triggered with: " + other.gameObject.name;
    }

}
