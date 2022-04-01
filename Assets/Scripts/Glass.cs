using System.Collections;
using UnityEngine;

public class Glass : MonoBehaviour, IBinInteractor<Bin>
{
    //Necessary components for filling and draining
    [SerializeField] private Transform particleDrainTranform;
    public Material fullGlassMaterial;
    [SerializeField] private Material emptyGlassMaterial;

    private MeshRenderer rend;
    public MeshRenderer Rend
    {
        get
        {
            return rend;
        }
        set
        {
            rend = value;
        }
    }

    private WaterController waterController;

    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void Init(WaterController _waterController)
    {
        waterController = _waterController;
    }

    //Cubic Ease In Function
    private IEnumerator GlassDown(float targetAngle_Z, float completeInSeconds)
    {
        float t = 0f;
        float startAngle_Z = transform.localEulerAngles.z;
        float diffAngle_Z = targetAngle_Z - startAngle_Z;
        float angle_Z;
        while (t < 1)
        {
            t += Time.deltaTime / completeInSeconds;
            angle_Z = diffAngle_Z * t * t * t + startAngle_Z;

            transform.rotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x,
                                                              transform.localEulerAngles.y,
                                                              angle_Z
            ));
            yield return null;
        }
        yield break;
    }

    //Quadratic Ease Out Function
    public IEnumerator WaterPlant(float completeInSeconds)
    {
        yield return StartCoroutine(GlassDown(45f, 1.5f));
        waterController.PlayWaterParticle(particleDrainTranform, .8f, transform);

        float startAngle_Z = transform.localEulerAngles.z;
        float diffAngle_Z = -startAngle_Z;

        float angle_Z;
        float t = 0f;
        while (t<completeInSeconds)
        {
            t += Time.deltaTime;
            angle_Z = -diffAngle_Z * t * (t - 2) + startAngle_Z;
            transform.rotation = Quaternion.Euler(new Vector3(transform.localEulerAngles.x,
                                                              transform.localEulerAngles.y,
                                                              angle_Z
            ));
            yield return null;
        }
        waterController.waterParticle.Stop();
        rend.material = emptyGlassMaterial;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bin _bin))
        {
            Interact(_bin);
        }
    }

    public void Interact(Bin bin)
    {
        bin.Execute();
    }
}
