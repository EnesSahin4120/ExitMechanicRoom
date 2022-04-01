using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Aim aim;
    private ThrowController throwController;
    private Draggable draggable;
    private Placer placer;
    private MouseInfo mouseInfo;
    private BoardDrawer boardDrawer;

    private void OnEnable()
    {
        DrawInfo.onDrawedBoard += SetWatering;
        WaterController.onWateredPlant += SetThrowing;
    }

    private void OnDisable()
    {
        DrawInfo.onDrawedBoard -= SetWatering;
        WaterController.onWateredPlant -= SetThrowing;
    }

    public void InitThrowComponents(ThrowController _throwController, Aim _aim)
    {
        aim = _aim;
        throwController = _throwController;
    }

    public void InitWaterComponents(Draggable _draggable, MouseInfo _mouseInfo, Placer _placer) 
    {
        draggable = _draggable;
        mouseInfo = _mouseInfo;
        placer = _placer;
    }

    public void InitPaintComponents(BoardDrawer _boardDrawer)
    {
        boardDrawer = _boardDrawer;
    }

    private void Start()
    {
        SetPainting();
    }

    private void SetPainting()
    {
        aim.enabled = false;
        aim.gameObject.SetActive(false);
        throwController.enabled = false;
        throwController.gameObject.SetActive(false);
        draggable.enabled = false;
        mouseInfo.enabled = false;
        placer.enabled = false;
    }

    private void SetWatering()
    {
        boardDrawer.enabled = false;
        draggable.enabled = true;
        mouseInfo.enabled = true;
        placer.enabled = true;
    }

    public void SetThrowing()
    {
        aim.gameObject.SetActive(true);
        aim.enabled = true;
        throwController.enabled = true;
        throwController.gameObject.SetActive(true);
    }
}
