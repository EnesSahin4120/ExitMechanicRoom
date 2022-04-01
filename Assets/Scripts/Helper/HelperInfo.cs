using UnityEngine;

public class HelperInfo : MonoBehaviour
{
    //Helper Info for arrow and arrow text
    public Transform[] arrowTransforms;
    public Transform[] targetTransforms;
    public Transform[] textTransforms;
    public string[] helperStrings;

    private int currentIndex = 0;
    public int CurrentIndex
    {
        get
        {
            return currentIndex;
        }
        set
        {
            currentIndex = value;
        }
    }
}
