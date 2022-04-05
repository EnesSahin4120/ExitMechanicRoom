using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    //Usable Ingame components
    private MouseInfo mouseInfo;
    private Placer placer;
    private BoardDrawer boardDrawer;
    private PaintDetectorController drawController;
    private DrawInfo drawInfo;
    private PaintDetector[] paintDetectors;
    private ThrowController throwController;
    private Aim aim;
    private GameManager gameManager;
    private Draggable draggable;
    private PaintController paintController;
    private WaterController waterController;
    private Bin bin;
    [SerializeField] private Glass glass;
    private HelperArrow helperArrow;
    private HelperInfo helperInfo;
    private Helper_UI helper_UI;

    private void Awake()
    {
        mouseInfo = FindObjectOfType<MouseInfo>();
        placer = FindObjectOfType<Placer>();
        boardDrawer = FindObjectOfType<BoardDrawer>();
        drawController = FindObjectOfType<PaintDetectorController>();
        drawInfo = FindObjectOfType<DrawInfo>();
        throwController = FindObjectOfType<ThrowController>();
        aim = FindObjectOfType<Aim>();
        gameManager = FindObjectOfType<GameManager>();
        draggable = FindObjectOfType<Draggable>();
        paintController = FindObjectOfType<PaintController>();
        waterController = FindObjectOfType<WaterController>();
        bin = FindObjectOfType<Bin>();
        helperArrow = FindObjectOfType<HelperArrow>();
        helperInfo = FindObjectOfType<HelperInfo>();
        helper_UI = FindObjectOfType<Helper_UI>();

        placer.Init(mouseInfo, waterController, helperInfo);
        drawController.Init(boardDrawer, drawInfo);
        gameManager.InitThrowComponents(throwController, aim);
        gameManager.InitWaterComponents(draggable, mouseInfo, placer);
        boardDrawer.Init(paintController);
        paintController.Init(helperInfo);
        gameManager.InitPaintComponents(boardDrawer);
        drawInfo.Init(helperInfo);
        waterController.Init(helperInfo);
        bin.Init(helperInfo);
        glass.Init(waterController);
        helperArrow.Init(helperInfo);
        helper_UI.Init(helperInfo);
    }

    private void Start()
    {
        paintDetectors = FindObjectsOfType<PaintDetector>();

        foreach(var current in paintDetectors)
        {
            current.Init(drawInfo);
        }
    }
}
