using UnityEngine;

public class BoardDrawer : MonoBehaviour
{
    [SerializeField] private LayerMask boardLayer;

    //Paint Info
    private const float linePointsMinDistance = 0.02f;
    public float lineWidth;
    [HideInInspector] public Paint currentLine;

    private PaintController paintController;

    public void Init(PaintController _paintController)
    {
        paintController = _paintController;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    private void BeginDraw()
    {
        if (paintController.IsSelectedBoard)
        {
            currentLine = ObjectPooler.Instance.SpawnToPool("Paint", transform.position, Quaternion.identity).GetComponent<Paint>();
            currentLine.transform.SetParent(transform);
            currentLine.SetLineWidth(lineWidth);
            currentLine.SetPointMinDistance(linePointsMinDistance);
        }
    }

    private void Draw()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, boardLayer))
            currentLine.AddPoint(hit.point);
        else
            EndDraw();
    }

    private void EndDraw()
    {
        if (currentLine != null){
            if (currentLine.pointsCount < 2)
                currentLine.gameObject.SetActive(false);
            else
                currentLine = null;
        }
    }
}
