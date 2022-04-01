using UnityEngine;

public class HelperArrow : MonoBehaviour
{
    private HelperInfo helperInfo;

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    private void OnEnable()
    {
        PaintController.onSelectedPen += UpdateTransform;
        DrawInfo.onDrawedBoard += UpdateTransform;
        Placer.onSettled_to_Dispenser += UpdateTransform;
        WaterController.onFilledGlass += UpdateTransform;
        Bin.onFilledBin += UpdateTransform;
    }

    private void OnDisable()
    {
        PaintController.onSelectedPen -= UpdateTransform;
        DrawInfo.onDrawedBoard -= UpdateTransform;
        Placer.onSettled_to_Dispenser -= UpdateTransform;
        WaterController.onFilledGlass -= UpdateTransform;
        Bin.onFilledBin -= UpdateTransform;

    }

    private void Start()
    {
        UpdateTransform();
    }

    //Update arrow position and rotation after mission completed
    private void UpdateTransform()
    {
        transform.position = helperInfo.arrowTransforms[helperInfo.CurrentIndex].position;
        Vector3 lookDir = (helperInfo.targetTransforms[helperInfo.CurrentIndex].position - transform.position).normalized;
        transform.right = -lookDir;
    }
}
