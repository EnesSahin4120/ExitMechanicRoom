using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    private LineRenderer lineRenderer;

    //Paint Info
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [HideInInspector] public int pointsCount = 0;
    private float pointsMinDistance = 0.1f;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public Vector3 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointsCount - 1);
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    public void AddPoint(Vector3 newPoint)
    {
        if (pointsCount >= 1 && Vector3.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        pointsCount++;

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);   
    }

    public void SetPointMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }
}
