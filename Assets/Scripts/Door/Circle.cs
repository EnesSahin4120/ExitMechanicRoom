using UnityEngine;

public class Circle : MonoBehaviour
{
    private SpriteRenderer rend;

    private bool isClickable;

    //User Grades
    [SerializeField] private float minA;
    [SerializeField] private float maxA;
    [SerializeField] private float changeSpeed;

    private SinusodialModify sinusodialModify;

    private void OnEnable()
    {
        Bin.onFilledBin += SetClickable;
    }

    private void OnDisable() 
    {
        Bin.onFilledBin -= SetClickable;
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sinusodialModify = new SinusodialModify(changeSpeed, maxA, minA);
    }

    private void Update()
    {
        if (isClickable)
            SetModifiedColor();
        else
            rend.color = new Color(1, 1, 1, 1);
    }

    //Modified color transparency in time change 
    private void SetModifiedColor()
    {
        float a;
        a = sinusodialModify.ModifiedNumerical();
        rend.color = new Color(1, 1, 1, a);
    }

    private void SetClickable()
    {
        isClickable = true;
    }
}
