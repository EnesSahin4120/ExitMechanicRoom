using UnityEngine;

public class WaterDispenser : MonoBehaviour 
{
    private bool isInteractable;
    public bool IsInteractable
    {
        get
        {
            return isInteractable;
        }
        set
        {
            isInteractable = value;
        }
    }

    //User Grades
    [SerializeField] private float animationSpeed;
    [SerializeField] private float bottomScaleNumerical;
    [SerializeField] private float topScaleNumerical;

    private SinusodialModify sinusodialModify;

    private void Start()
    {
        sinusodialModify = new SinusodialModify(animationSpeed, topScaleNumerical, bottomScaleNumerical);
    }

    private void Update()
    {
        if (isInteractable)
            SetModifiedScale();
        else
            transform.localScale = Vector3.one;
    }

    //Modified scale in time changing
    private void SetModifiedScale()
    {
        transform.localScale = new Vector3(sinusodialModify.ModifiedNumerical(),
                                               sinusodialModify.ModifiedNumerical(),
                                               sinusodialModify.ModifiedNumerical()
            );
    }
}
