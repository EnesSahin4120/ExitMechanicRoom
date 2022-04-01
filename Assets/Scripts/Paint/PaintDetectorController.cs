using UnityEngine;

public class PaintDetectorController : MonoBehaviour, IPaintDetectorInteractor<PaintDetector>
{
    [SerializeField] private BoxCollider board;

    //All numericals for spawning of detectors
    private float boardWidth;
    private float boardHeight;
    private float detectorRadius;
    private int spawnRowCount;
    private int spawnColumnCount;
    private Vector3 startSpawnPosition;

    private BoardDrawer boardDrawer;
    private DrawInfo drawInfo;

    public void Init(BoardDrawer _boardDrawer, DrawInfo _drawInfo)
    {
        boardDrawer = _boardDrawer;
        drawInfo = _drawInfo;
    }

    private void Awake()
    {
        GetDetectorInfo();
        drawInfo.necessaryPaintCount = spawnColumnCount * spawnRowCount;
        SpawnDrawDetectors();
    }

    private void Update()
    {
        DetectorHitControl();
    }

    private void GetDetectorInfo()
    {
        boardWidth = board.size.x;
        boardHeight = board.size.y;
        detectorRadius = boardDrawer.lineWidth / 2f;
        spawnRowCount = Mathf.FloorToInt(boardHeight / (2f * detectorRadius));
        spawnColumnCount = Mathf.FloorToInt(boardWidth / (2f * detectorRadius));
        startSpawnPosition = new Vector3(boardWidth / 2f - detectorRadius / 2f, boardHeight / 2f - detectorRadius / 2f, 0);
    }

    //Spawn draw detectors and set positions according to line width, board height and board width
    private void SpawnDrawDetectors()
    {
        for(int i = 0; i < spawnRowCount; i++)
        {
            for(int j = 0; j < spawnColumnCount; j++)
            {
                BoxCollider currentSpawnDetector = ObjectPooler.Instance.SpawnToPool("PaintDetector", transform.position, Quaternion.identity).GetComponent<BoxCollider>();
                currentSpawnDetector.transform.SetParent(board.transform);
                currentSpawnDetector.transform.localPosition = new Vector3(startSpawnPosition.x - j * detectorRadius * 2,
                                                                           startSpawnPosition.y - i * detectorRadius * 2,
                                                                           0
                );
                currentSpawnDetector.size = new Vector3(detectorRadius, detectorRadius, 0.1f);
            }
        }
    }

    //Click and interact with detector part
    private void DetectorHitControl()
    {
        if (boardDrawer.currentLine != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
                if(hit.collider.TryGetComponent(out PaintDetector _paintDetector)){
                    Interact(_paintDetector);
                }
            }
        }
    }

    public void Interact(PaintDetector paintDetector)
    {
        paintDetector.Execute();
    }
}
