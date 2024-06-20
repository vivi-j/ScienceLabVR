using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour
{

    public Transform tapObject; // Reference to the tap object in the scene

    void Start()
    {
        transform.LookAt(tapObject.position);
    }

}