using UnityEngine;
using UnityEngine.UI;

public class Helper_UI : MonoBehaviour
{
    private Text helperText;

    private HelperInfo helperInfo;

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    private void OnEnable()
    {
        PaintController.onSelectedPen += SetTextInfo;
        DrawInfo.onDrawedBoard += SetTextInfo;
        Placer.onSettled_to_Dispenser += SetTextInfo;
        WaterController.onFilledGlass += SetTextInfo;
        Bin.onFilledBin += SetTextInfo;
    }

    private void OnDisable()
    {
        PaintController.onSelectedPen -= SetTextInfo;
        DrawInfo.onDrawedBoard -= SetTextInfo;
        Placer.onSettled_to_Dispenser -= SetTextInfo;
        WaterController.onFilledGlass -= SetTextInfo;
        Bin.onFilledBin -= SetTextInfo;

    }
    private void Awake()
    {
        helperText = GetComponent<Text>();
    }

    private void Start()
    {
        SetTextInfo();
    }

    //Update helper text after mission completed
    private void SetTextInfo()
    {
        helperText.text = helperInfo.helperStrings[helperInfo.CurrentIndex].ToString();
        transform.position = helperInfo.textTransforms[helperInfo.CurrentIndex].position;
    }
}
