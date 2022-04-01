using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera gameCam;

    [SerializeField] private Transform[] targets;
    private int currentTargetIndex = 0;

    private void OnEnable()
    {
        DrawInfo.onDrawedBoard += ChangeCamera;
        DoorController.onOpenedDoor += ChangeCamera;
        Bin.onFilledBin += ChangeCamera;
        PaintController.onStartPainting += ChangeCamera;
        WaterController.onWateredPlant += ChangeCamera;
    }

    private void OnDisable()
    {
        DrawInfo.onDrawedBoard -= ChangeCamera;
        DoorController.onOpenedDoor -= ChangeCamera;
        Bin.onFilledBin -= ChangeCamera;
        PaintController.onStartPainting -= ChangeCamera;
        WaterController.onWateredPlant -= ChangeCamera;
    }

    private void Awake()
    {
        gameCam = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(Move_and_Look(targets[currentTargetIndex], 1.5f));
    }

    //Update camera positions and rotations after mission completed
    public void ChangeCamera()
    {
        if (currentTargetIndex != targets.Length - 1)
        {
            currentTargetIndex++;
            StartCoroutine(Move_and_Look(targets[currentTargetIndex], 1.5f));
        }
    }

    //Sinusodial Ease In-Out Function
    public IEnumerator Move_and_Look(Transform _target, float completeInSeconds)
    {
        Vector3 start = gameCam.transform.position;
        Vector3 startDir = gameCam.transform.forward;
        Vector3 diff = _target.position - start;
        Vector3 diffDir = _target.forward - startDir;

        float t = 0f;
        while (t < completeInSeconds)
        {
            t += Time.deltaTime;
            gameCam.transform.forward = -diffDir / 2f * (Mathf.Cos(Mathf.PI * t / completeInSeconds) - 1) + startDir;
            gameCam.transform.position = -diff / 2f * (Mathf.Cos(Mathf.PI * t / completeInSeconds) - 1) + start;
            yield return null;
        }
        yield break;
    }
}
