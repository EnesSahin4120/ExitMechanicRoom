using System.Collections;
using UnityEngine;
using System;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Circle circle;
    [SerializeField] private GameObject door;

    public static event Action onOpenedDoor;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CircleHitControl();
    }

    //Click to Circle and Open Door
    private void CircleHitControl()
    {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.collider.TryGetComponent(out Circle _circle)){
                if (circle = _circle){
                    OpenedDoor();
                }
            }
        }
    }

    //Exponential Ease In Function
    private IEnumerator OpenDoor(float targetAngle_Y,float completeInSeconds)
    {
        float startAngle_Y = door.transform.localEulerAngles.y - 90;
        float diffAngle_Y = targetAngle_Y - startAngle_Y;
        float angle_Y;

        float t = 0f;
        while (t < completeInSeconds)
        {
            t += Time.deltaTime;
            angle_Y = diffAngle_Y * Mathf.Pow(2, 10 * (t / completeInSeconds - 1)) + startAngle_Y;

            door.transform.rotation = Quaternion.Euler(door.transform.localEulerAngles.x,
                                                  angle_Y,
                                                  door.transform.localEulerAngles.z
            );
            yield return null;
        }
        yield break;
    }

    public void OpenedDoor()
    {
        circle.gameObject.SetActive(false);
        StartCoroutine(OpenDoor(-30, 2f));

        if (onOpenedDoor != null)
        {
            onOpenedDoor();
        }
    }
}
