using UnityEngine;

public class PaintDetector : MonoBehaviour, IDetectablePaint
{
    private DrawInfo drawInfo;

    public void Init(DrawInfo _drawInfo)
    {
        drawInfo = _drawInfo;
    }

    //Report painted info
    public void Execute()
    {
        drawInfo.currentPaintCount += 1;
        gameObject.SetActive(false);
        drawInfo.ControlDrawInfo();
    }
}
