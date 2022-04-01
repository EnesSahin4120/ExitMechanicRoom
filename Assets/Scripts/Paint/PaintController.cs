using UnityEngine;
using System;

public class PaintController : MonoBehaviour
{
    [SerializeField] private LayerMask pen;
    [SerializeField] private LayerMask board;

    private bool isSelectedPen;
    private bool isSelectedBoard;
    public bool IsSelectedBoard
    {
        get
        {
            return isSelectedBoard;
        }
        set
        {
            isSelectedBoard = value;
        }
    }

    //Selected pen info for painting
    private GameObject currentPen;
    private Vector3 firstPenPos;
    private Quaternion firstPenRot;

    private HelperInfo helperInfo;

    public static event Action onSelectedPen;
    public static event Action onStartPainting;

    private void OnEnable()
    {
        DrawInfo.onDrawedBoard += FinishPainting;
    }

    private void OnDisable()
    {
        DrawInfo.onDrawedBoard -= FinishPainting;
    }

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    private void Update()
    {
        if (currentPen)
            SetPenPosition();

        if (Input.GetMouseButtonDown(0))
            HitControl();
    }

    private void HitControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Selected pen part
        if (!isSelectedPen){
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, pen)){
                SetPenInfo(hit.collider.gameObject);
                SelectedPen();
            }
            return;
        }
        else
        {
            //Started painting part
            if (Physics.Raycast(ray, Mathf.Infinity, board) && !isSelectedBoard){
                isSelectedBoard = true;
                StartedPainting();
            }
        }
    }

    private void SetPenPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, board))
            currentPen.transform.position = hit.point + new Vector3(0, -0.15f, 0.01f);
    }

    private void SetPenInfo(GameObject targetPen)
    {
        isSelectedPen = true;
        currentPen = targetPen;
        firstPenPos = currentPen.transform.position;
        firstPenRot = currentPen.transform.rotation;
        currentPen.transform.rotation = Quaternion.Euler(0, 90, -120);
    }

    private void FinishPainting()
    {
        currentPen.transform.position = firstPenPos;
        currentPen.transform.rotation = firstPenRot;
        currentPen = null;
    }

    public void SelectedPen()
    {
        helperInfo.CurrentIndex++;
        if (onSelectedPen != null)
        {
            onSelectedPen();
        }
    }

    public void StartedPainting()
    {
        if (onStartPainting != null)
        {
            onStartPainting();
        }
    }
}
