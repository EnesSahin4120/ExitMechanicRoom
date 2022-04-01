using UnityEngine;
using System;

public class ThrowController : MonoBehaviour
{
    //Necessary components for throwing
    public Aim aim;
    public Rigidbody glass;

    private bool isThrowed;

    public static event Action onThrowed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isThrowed)
            Throwed();
    }

    //Projectile Motion ( x = x(i) + v(i) * t + 1/2 * g * t * t )
    public Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 diffVector = target - origin;
        Vector3 diffVector_XZ = diffVector;
        diffVector_XZ.y = 0f;

        float distance_Y = diffVector.y;
        float distance_XZ = diffVector_XZ.magnitude;

        float speed_XZ = distance_XZ / time;
        float speed_Y = distance_Y / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 resultVector = diffVector_XZ.normalized;
        resultVector *= speed_XZ;
        resultVector.y = speed_Y;

        return resultVector;
    }

    public void Throwed()
    {
        isThrowed = true;

        if (onThrowed != null)
        {
            onThrowed();
        }
    }
}
