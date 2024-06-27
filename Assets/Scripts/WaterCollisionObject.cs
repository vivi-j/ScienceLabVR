using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterCollisionObject : MonoBehaviour
{
    public TextMeshProUGUI collisionText;  // Reference to the TMP text component on the canvas

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        // Print the name of the collided GameObject to the TMP text component
        collisionText.text = "Collided with: " + other.name;
    }
}
