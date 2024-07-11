using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // The target to snap to

    void Start()
    {
        Snap();
    }

    public void Snap()
    {
       transform.position = target.position;
       transform.rotation = target.rotation;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
