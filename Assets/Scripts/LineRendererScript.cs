using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererScript : MonoBehaviour
{
    public Vector3 RelativeEndPoint;
    private Vector3 fixedVector;
    private LineRenderer lineRenderer;

    void Start()
    {
        fixedVector = transform.position + RelativeEndPoint;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, fixedVector);
    }
}
