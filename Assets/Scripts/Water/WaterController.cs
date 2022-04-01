using System.Collections;
using UnityEngine;
using System;

public class WaterController : MonoBehaviour
{
    //Components for water plant
    [SerializeField] private Glass glass;
    [SerializeField] private WaterDispenser waterDispenser;
    [SerializeField] private Transform particleFillTransform;
    public ParticleSystem waterParticle;

    private Draggable filledGlass;
    private HelperInfo helperInfo;

    public static event Action onWateredPlant;

    public void Init(HelperInfo _helperInfo)
    {
        helperInfo = _helperInfo;
    }

    public static event Action onFilledGlass;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DispenserHitControl();
    }

    //Ready for filling glass
    public void SetFillingProperties(Draggable _filledGlass)
    {
        waterDispenser.IsInteractable = true;
        filledGlass = _filledGlass;
        filledGlass.CanDraggable = false;
    }

    //Filling glass processing
    public IEnumerator WaterPlant_and_Finish()
    {
        if(glass.TryGetComponent(out Draggable currentDraggable))
            currentDraggable.CanDraggable = false;

        yield return glass.StartCoroutine(glass.WaterPlant(1f));
        WateredPlant();
    }

    public IEnumerator FillTheGlass(float completeInSeconds)
    {
        PlayWaterParticle(particleFillTransform, 0.5f, null);
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / completeInSeconds;
            yield return null;
        }
        FilledGlass();
        yield break;
    }

    private void DispenserHitControl() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
            if (hit.collider.TryGetComponent(out WaterDispenser _waterDispenser)){
                if (_waterDispenser==waterDispenser && _waterDispenser.IsInteractable)
                {
                    _waterDispenser.IsInteractable = false;
                    StartCoroutine(FillTheGlass(3f));
                }
            }
        }
    }

    //Filling glass is be completed
    public void FilledGlass()
    {
        filledGlass.CanDraggable = true;
        waterParticle.Stop();
        Glass current = filledGlass.GetComponent<Glass>();
        current.Rend.material = current.fullGlassMaterial;
        helperInfo.CurrentIndex++;
        if (onFilledGlass != null)
        {
            onFilledGlass();
        }
    }

    //Particle for filling glass and watering plant
    public void PlayWaterParticle(Transform targetPos,float _lifeTime,Transform parent)
    {
        var main = waterParticle.main;

        waterParticle.transform.SetParent(parent);
        waterParticle.transform.position = targetPos.position;
        main.startLifetime = _lifeTime;
        
        waterParticle.Play();
    }

    private void WateredPlant()
    {
        if (onWateredPlant != null)
        {
            onWateredPlant();
        }
    }
}
