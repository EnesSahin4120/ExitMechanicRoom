using UnityEngine;
using System;

public class Placer : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    private const float movingSpeedFactor = 0.003f;
    private bool isMove;

    //Curren Draggable Info
    private Draggable currentDraggable;
    private Vector3 currentMoveDirection;
    private Vector3 draggableStartPosition;

    private MouseInfo mouseInfo;
    private CameraController cameraController;
    private WaterController waterController;
    private HelperInfo helperInfo;

    public static event Action onSettled_to_Dispenser;

    public void Init(MouseInfo _mouseInfo, CameraController _cameraController, WaterController _waterController,HelperInfo _helperInfo)
    {
        mouseInfo = _mouseInfo;
        cameraController = _cameraController;
        waterController = _waterController;
        helperInfo = _helperInfo;
    }

    private void OnEnable()
    {
        MouseInfo.onPressed += SetCurrentDragObject;
        MouseInfo.onPressedUp += SetNewDraggableProperties;
    }

    private void OnDisable()
    {
        MouseInfo.onPressed -= SetCurrentDragObject;
        MouseInfo.onPressedUp -= SetNewDraggableProperties;
    }

    private void Update()
    {
        if (isMove)
            Move();
    }

    //Mouse click and Set current moving object
    private void SetCurrentDragObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.collider.TryGetComponent(out Draggable _draggable)){
                if (_draggable.CanDraggable){
                    currentDraggable = _draggable;
                    draggableStartPosition = currentDraggable.transform.position;
                    LookAtTarget_to_Move();
                }     
            }
        }
    }

    private void LookAtTarget_to_Move()
    {
        Vector3 targetPos = new Vector3(currentDraggable.CurrentTarget.transform.position.x,
                                        currentDraggable.transform.position.y,
                                        currentDraggable.CurrentTarget.transform.position.z);
        currentMoveDirection = (currentDraggable.transform.position - targetPos).normalized;
        isMove = true;
    }

    //Move towards to target according to Mouse difference
    private void Move()
    {
        float x = draggableStartPosition.x + currentMoveDirection.x * mouseInfo.DiffMousePos.x * movingSpeedFactor;
        float y = draggableStartPosition.y + mouseInfo.DiffMousePos.y * movingSpeedFactor;
        float z = draggableStartPosition.z + currentMoveDirection.z * mouseInfo.DiffMousePos.x * movingSpeedFactor;

        Vector3 position_to_Follow = new Vector3(x, y, z);
        currentDraggable.transform.position = Vector3.Lerp(currentDraggable.transform.position, position_to_Follow, movingSpeed * Time.deltaTime); 
    }

    //Click up and update
    private void SetNewDraggableProperties() 
    {
        if (currentDraggable != null){
            if (Vector3.Distance(currentDraggable.transform.position, currentDraggable.CurrentTarget.position) < .3f)
            {
                currentDraggable.transform.position = currentDraggable.CurrentTarget.position;
                if (currentDraggable.targets.Length - 1 != currentDraggable.CurrentTargetIndex){
                    if(currentDraggable.CurrentTargetIndex == currentDraggable.FillTheGlassIndex){
                        Settled_to_Dispenser(currentDraggable);
                    }
                    currentDraggable.UpdateInfo();

                    if (currentDraggable.CurrentTargetIndex == currentDraggable.WaterPlantIndex)
                        cameraController.ChangeCamera();
                }
                else
                    waterController.StartCoroutine(waterController.WaterPlant_and_Finish());
            }
            else
            {
                currentDraggable.transform.position = draggableStartPosition;
            }
            isMove = false;
            currentDraggable = null;
        }   
    }

    public void Settled_to_Dispenser(Draggable targetDraggable)
    {
        helperInfo.CurrentIndex++;
        waterController.SetFillingProperties(targetDraggable);
        if (onSettled_to_Dispenser != null)
        {
            onSettled_to_Dispenser();
        }
    }
}
