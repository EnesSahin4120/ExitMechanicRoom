using UnityEngine;

public class Draggable : MonoBehaviour
{
    public Transform[] targets;
    private Transform currentTarget;
    public Transform CurrentTarget
    {
        get
        {
            return currentTarget;
        }
        set
        {
            currentTarget = value;
        }
    }

    private int currentTargetIndex;
    public int CurrentTargetIndex
    {
        get
        {
            return currentTargetIndex;
        }
        set
        {
            currentTargetIndex = value;
        }
    }

    private int _waterPlantIndex = 1;
    public int WaterPlantIndex
    {
        get
        {
            return _waterPlantIndex;
        }
        set
        {
            _waterPlantIndex = value;
        }
    }
    private int _fillTheGlassIndex = 0;
    public int FillTheGlassIndex
    {
        get
        {
            return _fillTheGlassIndex;
        }
        set
        {
            _fillTheGlassIndex = value;
        }
    }

    private bool canDraggable = true;
    public bool CanDraggable
    {
        get
        {
            return canDraggable;
        }
        set
        {
            canDraggable = value;
        }
    }

    private void Start()
    {
        currentTarget = targets[currentTargetIndex];
    }

    //Update target info for drag and drop
    public void UpdateInfo()
    {
        currentTargetIndex++;
        currentTarget = targets[currentTargetIndex];
    }
}
