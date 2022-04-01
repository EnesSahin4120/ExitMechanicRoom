using UnityEngine;
using System;

public class Bin : MonoBehaviour, IInteractableBin
{
    private HelperInfo helperInfo;

    public static event Action onFilledBin; 

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    public void Execute()
    {
        FilledBin();
    }

    public void FilledBin()
    {
        helperInfo.CurrentIndex++;
        if (onFilledBin != null)
        {
            onFilledBin();
        }
    }
}
