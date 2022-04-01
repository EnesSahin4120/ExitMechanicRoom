using UnityEngine;
using System;

public class DrawInfo : MonoBehaviour
{
    //Painting Count Info
    [HideInInspector] public int necessaryPaintCount;
    [HideInInspector] public int currentPaintCount;

    private HelperInfo helperInfo;

    public static event Action onDrawedBoard;

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    public void ControlDrawInfo()
    {
        //Paint Completed
        if (currentPaintCount == necessaryPaintCount)
            DrawedBoard();
    }

    public void DrawedBoard()
    {
        helperInfo.CurrentIndex++;
        if (onDrawedBoard != null)
        {
            onDrawedBoard();
        }
    }
}
